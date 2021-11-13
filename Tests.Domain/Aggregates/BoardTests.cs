using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Enums;
using Faketory.Domain.Resources.IndustrialParts;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Aggregates
{
    public class BoardTests
    {
        [Fact]
        public async Task AddPallet_SinglePalletNoConflicts_ShouldAddPallet()
        {
            //arrange
            var board = new Board();
            var pallet = new Pallet(0, 0);
            var movedPallet = new MovedPallet(pallet); 
            
            //act
            board.AddPallets(new List<MovedPallet>{movedPallet});

            //assert
            pallet.PosX.Should().Be(0);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task AddPallet_PalletWithLowerPriority_ShouldUndoMovement()
        {
            //arrange
            var board = new Board();
            var firstPallet = new Pallet(0, 0);
            var firstMovedPallet = new MovedPallet(firstPallet);

            var pallet = new Pallet(0, 0);

            var movedPallet = new MovedPallet(pallet)
            {
                MovePriority = MovePriority.SameConveyor,
                PrevPosX = -1,
                PrevPosY = -1
            };

            //act
            board.AddPallets(new List<MovedPallet> {firstMovedPallet, movedPallet });

            //assert
            pallet.PosX.Should().Be(-1);
            pallet.PosY.Should().Be(-1);
            firstPallet.PosX.Should().Be(0);
            firstPallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task AddPallet_PalletWithHigherPriority_ShouldMovePallet()
        {
            //arrange
            var board = new Board();
            var firstPallet = new Pallet(0, 0);
            var firstMovedPallet = new MovedPallet(firstPallet)
            {
                PrevPosX = -1,
                PrevPosY = -1,
                MovePriority = MovePriority.ChangesConveyor
            };

            var pallet = new Pallet(0, 0);

            var movedPallet = new MovedPallet(pallet)
            {
                MovePriority = MovePriority.SameConveyor,
                PrevPosX = -1,
                PrevPosY = -1
            };

            //act
            board.AddPallets(new List<MovedPallet> { firstMovedPallet, movedPallet });

            //assert
            pallet.PosX.Should().Be(0);
            pallet.PosY.Should().Be(0);
            firstPallet.PosX.Should().Be(-1);
            firstPallet.PosY.Should().Be(-1);
        }

        [Fact]
        public async Task AddPallet_PalletsWithSamePriority_ShouldUndoRandomMovement()
        {
            //arrange
            var board = new Board();
            var firstPallet = new Pallet(0, 0);
            var firstMovedPallet = new MovedPallet(firstPallet)
            {
                MovePriority = MovePriority.ChangesConveyor,
                PrevPosX = -1,
                PrevPosY = -1
            };

            var pallet = new Pallet(0, 0);

            var movedPallet = new MovedPallet(pallet)
            {
                MovePriority = MovePriority.ChangesConveyor,
                PrevPosX = -1,
                PrevPosY = -1
            };

            //act
            board.AddPallets(new List<MovedPallet> { firstMovedPallet, movedPallet });

            //assert
            var pallets = board.GetPallets();
            pallets.Distinct().Should().HaveCount(2);
            pallets.Where(x => x.PosX == 0 && x.PosY == 0).Should().HaveCount(1);
        }

    }
}
