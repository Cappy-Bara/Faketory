using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Resources.IndustrialParts;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Conveyors
{
    public class MovePalletsTests
    {
        [Fact]
        public async Task MovePallets_ConveyorIsOff_PalletShouldntMove()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = false,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(0, 0)
            {
                Id = palletId,
            };

            //act
            await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosX.Should().Be(0);
            pallet.PosY.Should().Be(0);
        }

        [Theory]
        [InlineData(0,1)]
        [InlineData(1,2)]
        [InlineData(4,5)]
        public async Task MovePallets_ConveyorTurnedRight_PalletShouldMoveRight(int startPos, int finalPos)
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(startPos, 0)
            {
                Id = palletId,
            };

            //act
            await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosX.Should().Be(finalPos);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -2)]
        [InlineData(-4, -5)]
        public async Task MovePallets_ConveyorTurnedLeft_PalletShouldMoveLeft(int startPos, int finalPos)
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = true,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(startPos, 0)
            {
                Id = palletId,
            };

            //act
            await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosX.Should().Be(finalPos);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(4, 5)]
        public async Task MovePallets_ConveyorTurnedUp_PalletShouldMoveUp(int startPos, int finalPos)
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = true,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(0, startPos)
            {
                Id = palletId,
            };

            //act
            await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosY.Should().Be(finalPos);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -2)]
        [InlineData(-4, -5)]
        public async Task MovePallets_ConveyorTurnedDown_PalletShouldMoveUp(int startPos, int finalPos)
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(0, startPos)
            {
                Id = palletId,
            };

            //act
            await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosY.Should().Be(finalPos);
        }

        [Fact]
        public async Task MovePallets_ConveyorIsOff_PalletHasRightPriority()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = false,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(0, 0)
            {
                Id = palletId,
            };

            //act
            var movedPallets = await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            movedPallets.FirstOrDefault().MovePriority.Should().Be(MovePriority.Still);
        }

        [Fact]
        public async Task MovePallets_PalletsNull_ShouldNotThrowException()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = false,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var movedPallets = await conveyor.MovePallets(new List<Pallet>());

            //assert
            movedPallets.Should().BeEmpty();
        }

        [Fact]
        public async Task MovePallets_PalletsEmpty_ShouldNotThrowException()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = false,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };

            //act
            var movedPallets = await conveyor.MovePallets(null);

            //assert
            movedPallets.Should().BeEmpty();
        }

        [Fact]
        public async Task MovePallets_PalletOnConveyor_PalletHasRightPriority()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(2, 0)
            {
                Id = palletId,
            };

            //act
            var movedPallets = await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            movedPallets.FirstOrDefault().MovePriority.Should().Be(MovePriority.SameConveyor);
        }

        [Fact]
        public async Task MovePallets_PalletFallsFromConveyor_PalletHasRightPriority()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(4, 0)
            {
                Id = palletId,
            };

            //act
            var movedPallets = await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            movedPallets.FirstOrDefault().MovePriority.Should().Be(MovePriority.ChangesConveyor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task MovePallets_SelectedFrequency_PalletShouldMoveByOne(int freq)
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = freq,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var palletId = Guid.Parse("0e841655-6920-45c0-a71f-59232bf15b9e");
            var pallet = new Pallet(0, 0)
            {
                Id = palletId,
            };

            //act
            for(int i = 0;i<=freq;i++)
                await conveyor.MovePallets(new List<Pallet> { pallet });

            //assert
            pallet.PosX.Should().Be(1);
        }
    }
}
