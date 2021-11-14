using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Sensors
{
    public class SenseTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Sense_NoPallets_SensorShouldntSense(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                NegativeLogic = negativeLogic
            };

            //act
            sensor.Sense(new List<Pallet>());

            //assert
            sensor.IsSensing.Should().Be(negativeLogic);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Sense_NullPallets_SensorShouldntSenseAndNoExceptionThrown(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                NegativeLogic = negativeLogic
            };

            //act
            sensor.Sense(null);

            //assert
            sensor.IsSensing.Should().Be(negativeLogic);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task Sense_PalletOnNegativeLogicSensor_SensorShouldSense(bool negativeLogic)
        {
            //arrange
            var sensor = new Sensor()
            {
                PosX = 0,
                PosY = 0,
                NegativeLogic = negativeLogic
            };
            var pallet = new Pallet(0, 0);

            //act
            sensor.Sense(new List<Pallet> {pallet});

            //assert
            sensor.IsSensing.Should().Be(!negativeLogic);
        }
    }
}
