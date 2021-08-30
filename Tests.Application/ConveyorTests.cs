using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Faketory.Infrastructure.DbContexts;
using Faketory.Domain.Resources.IndustrialParts;
using Microsoft.EntityFrameworkCore;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Domain.Enums;
using Faketory.Domain.IRepositories;
using Faketory.Infrastructure.Repositories;
using Faketory.Application.Services;

namespace Tests.Application
{
    public class ConveyorTests 
    { 
        public async Task<FaketoryDbContext> GetDbContext(List<IO> IOs,List<Conveyor> conveyors, List<ConveyingPoint> convPoints, List<Pallet> pallets)
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            await context.InputsOutputs.AddRangeAsync(IOs);
            await context.Conveyors.AddRangeAsync(conveyors);
            await context.ConveyingPoints.AddRangeAsync(convPoints);
            await context.Pallets.AddRangeAsync(pallets);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task SceneHandlerTest_SimpleSceneCreated_PalletShoudMove()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Output, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyors = new List<Conveyor> { new Conveyor(0, 0, 5, 0, false, false, email) {IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyingPoints = new List<ConveyingPoint>();
            var pallets = new List<Pallet> { new Pallet { PosX = 0, PosY = 0, UserEmail = email } };

            foreach (Conveyor c in conveyors)
                conveyingPoints.AddRange(c.ConveyingPoints);

            var context = await GetDbContext(ios,conveyors,conveyingPoints,pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, email);
            //act
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();

            var pallet = (await palletRepo.GetAllUserPallets(email)).FirstOrDefault();

            //assert
            pallet.PosX.Should().Be(2);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task SceneHandlerTest_SimpleSceneCreated_PalletShoudNotMove()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = false, Type = IOType.Output, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyors = new List<Conveyor> { new Conveyor(0, 0, 5, 0, false, false, email) { IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyingPoints = new List<ConveyingPoint>();
            var pallets = new List<Pallet> { new Pallet { PosX = 0, PosY = 0, UserEmail = email } };

            foreach (Conveyor c in conveyors)
                conveyingPoints.AddRange(c.ConveyingPoints);

            var context = await GetDbContext(ios, conveyors, conveyingPoints, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, email);
            //act
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();

            var pallet = (await palletRepo.GetAllUserPallets(email)).FirstOrDefault();

            //assert
            pallet.PosX.Should().Be(0);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task SceneHandlerTest_PalletBlocksSecondOne_PalletShoudNotMove()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Output, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyors = new List<Conveyor> { new Conveyor(0, 0, 5, 0, false, false, email) { IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyingPoints = new List<ConveyingPoint>();
            var pallets = new List<Pallet> { new Pallet { PosX = 5, PosY = 0, UserEmail = email, Id = Guid.Parse("bda6726b-6d90-4d78-a071-4f24769e2063") },
                new Pallet { PosX = 6, PosY = 0, UserEmail = email } };

            foreach (Conveyor c in conveyors)
                conveyingPoints.AddRange(c.ConveyingPoints);

            var context = await GetDbContext(ios, conveyors, conveyingPoints, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, email);
            //act
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();

            var pallet = (await palletRepo.GetAllUserPallets(email))
                .FirstOrDefault(x => x.Id == Guid.Parse("bda6726b-6d90-4d78-a071-4f24769e2063"));

            //assert
            pallet.PosX.Should().Be(5);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task SceneHandlerTest_TwoPalletsOnConveyor_PalletsShoudMove()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Output, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyors = new List<Conveyor> { new Conveyor(0, 0, 5, 0, false, false, email) { IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var conveyingPoints = new List<ConveyingPoint>();
            var pallets = new List<Pallet> { new Pallet { PosX = 0, PosY = 0, UserEmail = email, Id = Guid.Parse("bda6726b-6d90-4d78-a071-4f24769e2063") },
                new Pallet { PosX = 1, PosY = 0, UserEmail = email, Id = Guid.Parse("0537488d-8994-4f55-90fe-e098ad32b2a2")} };

            foreach (Conveyor c in conveyors)
                conveyingPoints.AddRange(c.ConveyingPoints);

            var context = await GetDbContext(ios, conveyors, conveyingPoints, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, email);
            //act
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();
            await sceneHandler.Timestamp();

            var pallet1 = (await palletRepo.GetAllUserPallets(email))
                .FirstOrDefault(x => x.Id == Guid.Parse("bda6726b-6d90-4d78-a071-4f24769e2063"));

            var pallet2 = (await palletRepo.GetAllUserPallets(email))
                .FirstOrDefault(x => x.Id == Guid.Parse("0537488d-8994-4f55-90fe-e098ad32b2a2"));

            //assert
            pallet1.PosX.Should().Be(3);
            pallet1.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(4);
            pallet2.PosY.Should().Be(0);
        }
    }
}
