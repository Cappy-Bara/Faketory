using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Aggregates
{
    public class MovedPalletTests
    {
        [Fact]
        public void UndoMovement_PalletMovementRewind_ShouldHavePreviousPosition()
        {
            //arrange
            var pallet = new Pallet(0,0)
            {
                Id = Guid.Parse("b23a01c5-c79a-4540-9746-58d460916aad"),
            };
            var conveyor = new Conveyor()
            {
                IsVertical = false,
                IsRunning = true,
                Frequency = 0,
                IsTurnedDownOrLeft = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            //act
            var movedPallet = conveyor.MovePallets(new List<Pallet>{ pallet });
            movedPallet.FirstOrDefault().UndoMovement();
            //assert
            pallet.PosX.Should().Be(0);
            pallet.PosY.Should().Be(0);
        }
    }
}
