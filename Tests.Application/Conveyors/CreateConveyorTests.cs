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

namespace Tests.Application.Conveyors
{
    public class CreateConveyorTests
    {
        private static async Task<FaketoryDbContext> GetDbContext(List<IO> IOs, List<Conveyor> conveyors)
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            await context.InputsOutputs.AddRangeAsync(IOs);
            await context.Conveyors.AddRangeAsync(conveyors);
            await context.SaveChangesAsync();
            return context;
        }
        private static CreateConveyorHandler CreateHandler(IIORepository IORepo, IConveyorRepository ConveyorRepo, ISlotRepository SlotRepo = null, IUserRepository UserRepo = null)
        {
            var SlotRepoMock = new Mock<ISlotRepository>();
            SlotRepoMock.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepoMock = new Mock<IUserRepository>();
            UserRepoMock.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);

            return new CreateConveyorHandler(UserRepo ?? UserRepoMock.Object, IORepo, ConveyorRepo, SlotRepo ?? SlotRepoMock.Object);
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
            var ios = new List<IO>{ io };
            var context = await GetDbContext(ios, new List<Conveyor>());
            var ioRepo = new IORepository(context);
            var conveyorRepo = new ConveyorRepository(context);

            var sut = CreateHandler(ioRepo, conveyorRepo);
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
            var id = await sut.Handle(command, CancellationToken.None);
            //Assert
            id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateConveyorHandler_IODoesntExist_ConveyorAndIOShouldBeCreated()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>());
            var ioRepo = new IORepository(context);
            var conveyorRepo = new ConveyorRepository(context);

            var sut = CreateHandler(ioRepo, conveyorRepo);
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
            var id = await sut.Handle(command, CancellationToken.None);

            //Assert
            var io = await ioRepo.GetIO(Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"), 0, 0, IOType.Output);
            io.Should().NotBeNull();
            id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateConveyorHandler_ConveyorsCollides_ShouldThrowException()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>());
            var ioRepo = new IORepository(context);
            var conveyorRepo = new ConveyorRepository(context);

            var sut = CreateHandler(ioRepo, conveyorRepo);
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

        [Fact]
        public async Task CreateConveyorHandler_SlotDoesntExist_ShouldThrowException()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>());
            var ioRepo = new IORepository(context);
            var conveyorRepo = new ConveyorRepository(context);
            var slotRepo = new SlotRepository(context);

            var sut = CreateHandler(ioRepo, conveyorRepo, slotRepo);
            var command = new CreateConveyorCommand()
            {
                Bit = 0,
                Byte = 0,
                Frequency = 1,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                SlotId = Guid.Parse("696daec8-e2cf-4bcc-bf6f-ed30a48fafc2"),
                Length = 4,
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //Act and Assert
            await sut.Invoking(async x => await x.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateConveyorHandler_UserDoesntExist_ShouldThrowException()
        {
            //arrange
            var context = await GetDbContext(new List<IO>(), new List<Conveyor>());
            var ioRepo = new IORepository(context);
            var conveyorRepo = new ConveyorRepository(context);
            var userRepo = new UserRepository(context);

            var sut = CreateHandler(ioRepo, conveyorRepo, default, userRepo);
            var command = new CreateConveyorCommand()
            {
                Bit = 0,
                Byte = 0,
                Frequency = 1,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                SlotId = Guid.Parse("696daec8-e2cf-4bcc-bf6f-ed30a48fafc2"),
                Length = 4,
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //Act and Assert
            await sut.Invoking(async x => await x.Handle(command, CancellationToken.None)).Should().ThrowAsync<NotFoundException>();
        }
    }

}
