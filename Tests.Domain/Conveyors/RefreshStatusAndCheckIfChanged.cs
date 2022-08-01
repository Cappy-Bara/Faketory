using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Conveyors
{
    public class RefreshStatusAndCheckIfChanged
    {
        [Theory]
        [InlineData(true,false,true,false)]
        [InlineData(false,false,true,true)]
        [InlineData(false,true,false,false)]
        [InlineData(true,true,false,true)]
        public void RefreshStatusAndCheckIfChanged_InputChangedValue_ShouldChangeConveyorState(bool outputState, bool stateBefore, bool requiredState, bool negativeLogic)
        {
            //arrange
            var io = new IO()
            {
                Type = IOType.Output,
                Value = outputState,
            };

            var conveyor = new Conveyor()
            {
                NegativeLogic = negativeLogic,
                IsRunning = stateBefore,
                IO = io
            };

            //act
            var modified = conveyor.RefreshStatusAndCheckIfChanged();

            //assert
            conveyor.IsRunning.Should().Be(requiredState);
            modified.Should().BeTrue();
        }

        [Theory]
        [InlineData(true,true,false)]
        [InlineData(false,true,true)]
        public void RefreshStatusAndCheckIfChanged_InputValueStable_ShouldNotChangeConveyorState(bool outputState, bool stateBefore, bool negativeLogic)
        {
            //arrange
            var io = new IO()
            {
                Type = IOType.Output,
                Value = outputState,
            };

            var conveyor = new Conveyor()
            {
                NegativeLogic = negativeLogic,
                IsRunning = stateBefore,
                IO = io
            };

            //act
            var modified = conveyor.RefreshStatusAndCheckIfChanged();

            //assert
            conveyor.IsRunning.Should().Be(stateBefore);
            modified.Should().BeFalse();
        }
    }
}
