using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Application.Resources.Conveyors.Commands.CreateConveyor;
using Faketory.Domain.Enums;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests.Application
{
    public class CreateConveyorTests
    {
        public async Task<FaketoryDbContext> GetDbContext(List<IO> IOs, List<Conveyor> conveyors, List<ConveyingPoint> convPoints)
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            await context.InputsOutputs.AddRangeAsync(IOs);
            await context.Conveyors.AddRangeAsync(conveyors);
            await context.ConveyingPoints.AddRangeAsync(convPoints);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task CreateConveyorHandler_IOExist_ConveyorShouldBeCreated()
        {
            //arrange
            var io = new IO()
            {
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Type = IOType.Output,
            };
            var ios = new List<IO>();
            ios.Add(io);

            var context = await GetDbContext(ios,new List<Conveyor>(),new List<ConveyingPoint>());
            var IORepo = new IORepository(context);
            var CPRepo = new ConveyingPointRepository(context);
            var ConveyorRepo = new ConveyorRepository(context);
            
            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);


            var sut = new CreateConveyorHandler(UserRepo.Object, IORepo, CPRepo, ConveyorRepo, SlotRepo.Object);

            var command = new CreateConveyorCommand()
            {
                Bit = 0,
                Byte = 0,
                Frequency = 1,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Length = 4,
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            var points = await CPRepo.GetAllUserConveyingPoints("test@gmail.com");
            points.Any().Should().BeTrue();
            points.Where(x => x.LastPoint).Count().Should().Be(1);
            points.Where(x => !x.LastPoint).Count().Should().Be(3);
            points.FirstOrDefault(x => x.LastPoint).PosX.Should().Be(0);
            points.FirstOrDefault(x => x.LastPoint).PosY.Should().Be(-3);
        }

        [Fact]
        public async Task CreateConveyorHandler_IODoesntExist_ConveyorAndIOShouldBeCreated()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>(), new List<ConveyingPoint>());
            var IORepo = new IORepository(context);
            var CPRepo = new ConveyingPointRepository(context);
            var ConveyorRepo = new ConveyorRepository(context);

            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);


            var sut = new CreateConveyorHandler(UserRepo.Object, IORepo, CPRepo, ConveyorRepo, SlotRepo.Object);

            var command = new CreateConveyorCommand()
            {
                Bit = 0,
                Byte = 0,
                Frequency = 1,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Length = 4,
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            var points = await CPRepo.GetAllUserConveyingPoints("test@gmail.com");
            var io = await IORepo.GetIO(Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"), 0, 0, IOType.Output);
            io.Should().NotBeNull();
            points.Any().Should().BeTrue();
            points.Where(x => x.LastPoint).Count().Should().Be(1);
            points.Where(x => !x.LastPoint).Count().Should().Be(3);
            points.FirstOrDefault(x => x.LastPoint).PosX.Should().Be(0);
            points.FirstOrDefault(x => x.LastPoint).PosY.Should().Be(-3);
        }

        [Fact]
        public async Task CreateConveyorHandler_ConveyorsCollides_ShouldThrowException()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>(), new List<ConveyingPoint>());
            var IORepo = new IORepository(context);
            var CPRepo = new ConveyingPointRepository(context);
            var ConveyorRepo = new ConveyorRepository(context);

            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);


            var sut = new CreateConveyorHandler(UserRepo.Object, IORepo, CPRepo, ConveyorRepo, SlotRepo.Object);

            var command = new CreateConveyorCommand()
            {
                Bit = 0,
                Byte = 0,
                Frequency = 1,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Length = 4,
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //act
            await sut.Handle(command, CancellationToken.None);
            //Assert
            await sut.Invoking(async x => await x.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotCreatedException>();
        }





    }
}
