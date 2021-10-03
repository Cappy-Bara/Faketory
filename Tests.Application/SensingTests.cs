using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Application.Services;
using Faketory.Domain.Enums;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Application
{
    public class SensingTests
    {
        public async Task<FaketoryDbContext> GetDbContext(List<IO> IOs, List<Sensor> sensors, List<Pallet> pallets)
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            await context.InputsOutputs.AddRangeAsync(IOs);
            await context.Pallets.AddRangeAsync(pallets);
            await context.Sensors.AddRangeAsync(sensors);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task SceneHandlerTest_PalletOnSensor_SensorShouldSense()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Input, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var pallets = new List<Pallet> { new Pallet { PosX = 0, PosY = 0, UserEmail = email } };
            var sensors = new List<Sensor> { new Sensor { PosX = 0, PosY = 0, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967"), IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966"),UserEmail = email} };

            var context = await GetDbContext(ios, sensors, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);
            ISensorRepository sensorRepo = new SensorRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, sensorRepo, email);
            //act
            await sceneHandler.Timestamp();

            var sensor = (await sensorRepo.GetSensorById(Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967")));

            //assert
            sensor.IsSensing.Should().BeTrue();
        }

        [Fact]
        public async Task SceneHandlerTest_DifferentUserPalletOnSensor_SensorShouldNotSense()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Input, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var pallets = new List<Pallet> { new Pallet { PosX = 0, PosY = 0, UserEmail = "different@email" } };
            var sensors = new List<Sensor> { new Sensor { PosX = 0, PosY = 0, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967"), IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966"), UserEmail = email } };

            var context = await GetDbContext(ios, sensors, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);
            ISensorRepository sensorRepo = new SensorRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, sensorRepo, email);
            //act
            await sceneHandler.Timestamp();

            var sensor = (await sensorRepo.GetSensorById(Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967")));

            //assert
            sensor.IsSensing.Should().BeFalse();
        }

        [Fact]
        public async Task SceneHandlerTest_PalletNotOnSensor_SensorShouldNotSense()
        {
            string email = "email@gmail.com";

            //arrange
            var ios = new List<IO> { new IO() { Value = true, Type = IOType.Input, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966") } };
            var pallets = new List<Pallet> { new Pallet { PosX = 3, PosY = 0, UserEmail = email } };
            var sensors = new List<Sensor> { new Sensor { PosX = 0, PosY = 0, Id = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967"), IOId = Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d966"), UserEmail = email } };

            var context = await GetDbContext(ios, sensors, pallets);

            IConveyorRepository convRepo = new ConveyorRepository(context);
            IPalletRepository palletRepo = new PalletRepository(context);
            IConveyingPointRepository convPointRepo = new ConveyingPointRepository(context);
            ISensorRepository sensorRepo = new SensorRepository(context);

            SceneHandler sceneHandler = new SceneHandler(palletRepo, convRepo, convPointRepo, sensorRepo, email);
            //act
            await sceneHandler.Timestamp();

            var sensor = (await sensorRepo.GetSensorById(Guid.Parse("6786fa06-c61c-474e-9f97-baf28288d967")));

            //assert
            sensor.IsSensing.Should().BeFalse();
        }
    }
}
