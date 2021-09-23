using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Application.Resources.Sensors.Commands.CreateSensor;
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
    public class CreateSensorTests
    {
        public async Task<FaketoryDbContext> GetDbContext(List<IO> IOs = null, List<Sensor> sensors = null)
        {
            IOs ??= new List<IO>();
            sensors ??= new List<Sensor>();

            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            await context.InputsOutputs.AddRangeAsync(IOs);
            await context.Sensors.AddRangeAsync(sensors);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task CreateSensorHandler_IOExist_SensorShouldBeCreated()
        {
            //arrange
            var io = new IO()
            {
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Type = IOType.Input,
            };
            var ios = new List<IO>();
            ios.Add(io);

            var context = await GetDbContext(ios);
            var IORepo = new IORepository(context);
            var sensorRepo = new SensorRepository(context);

            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);

            var sut = new CreateSensorHandler(SlotRepo.Object, IORepo, UserRepo.Object,sensorRepo);

            var command = new CreateSensorCommand()
            {
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //act
            await sut.Handle(command, CancellationToken.None);
            //Assert
            var sensors = await sensorRepo.GetUserSensors("test@gmail.com");
            sensors.Should().NotBeNullOrEmpty();
            sensors.Any().Should().BeTrue();
            sensors.FirstOrDefault().Should().NotBeNull();
        }
        [Fact]
        public async Task CreateSensorHandler_IODoesntExist_SensorAndIoShouldBeCreatedd()
        {
            var context = await GetDbContext();
            var IORepo = new IORepository(context);
            var sensorRepo = new SensorRepository(context);

            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);

            var sut = new CreateSensorHandler(SlotRepo.Object, IORepo, UserRepo.Object, sensorRepo);

            var command = new CreateSensorCommand()
            {
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //act
            await sut.Handle(command, CancellationToken.None);
            //Assert
            var sensors = await sensorRepo.GetUserSensors("test@gmail.com");
            sensors.Should().NotBeNullOrEmpty();
            sensors.Any().Should().BeTrue();
            sensors.FirstOrDefault().Should().NotBeNull();
            var IOExist = await IORepo.IOExists(Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"), 0, 0, IOType.Input);
            IOExist.Should().BeTrue();
        }
        [Fact]
        public async Task CreateSensorHandler_IOOccupied_ShouldThrowException()
        {
            //arrange
            var io = new IO()
            {
                Id = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c22"),
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                Type = IOType.Input,
            };
            var ios = new List<IO>();
            ios.Add(io);

            var sensor = new Sensor()
            {
                IOId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c22"),
                PosX = 0,
                PosY = 1,
                UserEmail = "asdas",
            };
            var sensors = new List<Sensor>();
            sensors.Add(sensor);


            var context = await GetDbContext(ios,sensors);
            var IORepo = new IORepository(context);
            var sensorRepo = new SensorRepository(context);

            var SlotRepo = new Mock<ISlotRepository>();
            SlotRepo.Setup(x => x.SlotExists(It.IsAny<Guid>()).Result).Returns(true);
            var UserRepo = new Mock<IUserRepository>();
            UserRepo.Setup(x => x.UserExists(It.IsAny<String>()).Result).Returns(true);

            var sut = new CreateSensorHandler(SlotRepo.Object, IORepo, UserRepo.Object,sensorRepo);

            var command = new CreateSensorCommand()
            {
                Bit = 0,
                Byte = 0,
                SlotId = Guid.Parse("eca29617-553c-4451-92bf-82795b3c2c23"),
                PosX = 0,
                PosY = 0,
                UserEmail = "test@gmail.com"
            };
            //Act & Assert
            await sut.Invoking(async x => await x.Handle(command, CancellationToken.None)).Should().ThrowAsync<OccupiedException>();
        }
    }
}
