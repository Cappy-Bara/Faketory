﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Conveyors
{
    public class GetOccupiedPointsTests
    {
        [Fact]
        public void GetOccupiedPoints_ConveyorTurnedRight_ShouldReturnValidPoints()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var points = conveyor.OccupiedPoints;
            //assert
            points.Where(x => x.Item2 == 0).ToList().Should().HaveCount(5);
            points.Distinct().ToList().Should().HaveCount(5);
            points.Where(x => x.Item1 >= 0).Should().HaveCount(5);
        }

        [Fact]
        public void GetOccupiedPoints_ConveyorTurnedLeft_ShouldReturnValidPoints()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsTurnedDownOrLeft = true,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var points = conveyor.OccupiedPoints;
            //assert
            points.Where(x => x.Item2 == 0).ToList().Should().HaveCount(5);
            points.Distinct().ToList().Should().HaveCount(5);
            points.Where(x => x.Item1 <= 0).Should().HaveCount(5);
        }

        [Fact]
        public void GetOccupiedPoints_ConveyorTurnedUp_ShouldReturnValidPoints()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsTurnedDownOrLeft = false,
                IsVertical = true,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var points = conveyor.OccupiedPoints;
            //assert
            points.Where(x => x.Item2 >= 0).ToList().Should().HaveCount(5);
            points.Distinct().ToList().Should().HaveCount(5);
            points.Where(x => x.Item1 == 0).Should().HaveCount(5);
        }

        [Fact]
        public void GetOccupiedPoints_onveyorTurnedDown_ShouldReturnValidPoints()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var points = conveyor.OccupiedPoints;
            //assert
            points.Where(x => x.Item2 <= 0).ToList().Should().HaveCount(5);
            points.Distinct().ToList().Should().HaveCount(5);
            points.Where(x => x.Item1 == 0).Should().HaveCount(5);
        }
    }
}
