using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Services
{
    public class SensorSenseServiceTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Sense_NoPallets_SensorShouldntSense(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                PosX = 0,
                PosY = 0,
                NegativeLogic = negativeLogic
            };
            var sut = new SensingService(new List<Pallet>(), new List<Sensor> { sensor });

            //act
            sut.HandleSensing();

            //assert
            sensor.IsSensing.Should().Be(negativeLogic);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Sense_NullPallets_SensorShouldntSenseAndNoExceptionThrown(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                PosX = 0,
                PosY = 0,
                NegativeLogic = negativeLogic
            };
            var sut = new SensingService(null, new List<Sensor> { sensor });

            //act
            sut.HandleSensing();

            //assert
            sensor.IsSensing.Should().Be(negativeLogic);
        }

        [Fact]
        public void Sense_NoSensors_ShouldNotThrowException()
        {
            //arrange
            var service = new SensingService(new List<Pallet>(), new List<Sensor>());
            Action sut = () => service.HandleSensing();
            //act & assert
            sut.Should().NotThrow();
        }

        [Fact]
        public void Sense_NullSensors_ShouldNotThrowException()
        {
            //arrange
            var service = new SensingService(new List<Pallet>(), null);
            Action sut = () => service.HandleSensing();
            //act & assert
            sut.Should().NotThrow();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Sense_PalletOnSensor_SensorShouldSenseInBothLogics(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                PosX = 0,
                PosY = 0,
                NegativeLogic = negativeLogic
            };
            var pallet = new Pallet(0, 0);
            var sut = new SensingService(new List<Pallet> {pallet }, new List<Sensor> { sensor });

            //act
            sut.HandleSensing();

            //assert
            sensor.IsSensing.Should().Be(!negativeLogic);
        }

        [Fact]
        public void Sense_SomePalletsAndSensors_ShouldSetRightValues()
        {
            //arrange
            var sensor = new Sensor()
            {
                PosX = 0,
                PosY = 0,
                NegativeLogic = false,
            };
            var sensor2 = new Sensor()
            {
                PosX = 1,
                PosY = 0,
                NegativeLogic = false,

            };
            var sensor3 = new Sensor()
            {
                PosX = 1,
                PosY = 1,
                NegativeLogic = false,

            };
            var pallet = new Pallet(0, 0);
            var pallet2 = new Pallet(1, 1);
            var sut = new SensingService(new List<Pallet> { pallet,pallet2 }, new List<Sensor> { sensor,sensor2,sensor3 });

            //act
            sut.HandleSensing();

            //assert
            sensor.IsSensing.Should().BeTrue();
            sensor2.IsSensing.Should().BeFalse();
            sensor3.IsSensing.Should().BeTrue();
        }


    }
}
