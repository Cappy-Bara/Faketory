using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Aggregates
{
    public class ConveyorMovementServiceTests
    {
        [Fact]
        public async Task HandleConveyorMovement_PalletOnConveyor_PalletShouldMove()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var pallet = new Pallet(0, 0);
            var sut = new ConveyorMovementService(new List<Conveyor> {conveyor}, new List<Pallet> {pallet });
            
            //act
            await sut.HandleConveyorMovement();
            
            //assert
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task HandleConveyorMovement_PalletsOnConveyor_PalletsShouldMove()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var pallet = new Pallet(0, 0);
            var pallet2 = new Pallet(1, 0);
            var sut = new ConveyorMovementService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet,pallet2 });

            //act
            await sut.HandleConveyorMovement();

            //assert
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(2);
            pallet2.PosY.Should().Be(0);
        }

        [Fact]
        public async Task HandleConveyorMovement_PalletNearConveyor_PalletShouldStay()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var pallet = new Pallet(6, 0);
            var sut = new ConveyorMovementService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet});

            //act
            await sut.HandleConveyorMovement();

            //assert
            pallet.PosX.Should().Be(6);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public async Task HandleConveyorMovement_PalletBlocksPallet_PalletsShouldStay()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 5,
                PosX = 0,
                PosY = 0,
            };
            var pallet = new Pallet(4, 0);
            var pallet2 = new Pallet(5, 0);
            var sut = new ConveyorMovementService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet,pallet2 });
            //act
            await sut.HandleConveyorMovement();

            //assert
            pallet.PosX.Should().Be(4);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(5);
            pallet2.PosY.Should().Be(0);
        }

        [Fact]
        public async Task HandleConveyorMovement_TwoPalletsFallToTheSamePlace_OnePalletShouldFall()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 3,
                PosX = 0,
                PosY = 0,
            };
            var conveyor2 = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = true,
                IsVertical = false,
                Frequency = 0,
                Length = 3,
                PosX = 6,
                PosY = 0,
            };

            var pallet = new Pallet(2, 0);
            var pallet2 = new Pallet(4, 0);
            var pallets = new List<Pallet> { pallet, pallet2 };

            var sut = new ConveyorMovementService(new List<Conveyor> { conveyor, conveyor2 }, pallets);
            
            //act
            await sut.HandleConveyorMovement();

            //assert
            pallets.Count.Should().Be(2);
            pallets.Where(x => x.PosX == 3).Should().HaveCount(1);
        }

        [Fact]
        public async Task HandleConveyorMovement_CollisionWhileMovingBetweenConveyors_OnePalletShouldWait()
        {
            //arrange
            var conveyor = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = false,
                IsVertical = false,
                Frequency = 0,
                Length = 3,
                PosX = 0,
                PosY = 0,
            };
            var conveyor2 = new Conveyor()
            {
                IsRunning = true,
                IsTurnedDownOrLeft = true,
                IsVertical = true,
                Frequency = 0,
                Length = 3,
                PosX = 1,
                PosY = 0,
            };

            var pallet = new Pallet(0, 0);
            var pallet2 = new Pallet(1, 1);

            var sut = new ConveyorMovementService(new List<Conveyor> { conveyor, conveyor2 }, new List<Pallet> { pallet, pallet2 });

            //act
            await sut.HandleConveyorMovement();

            //assert
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(1);
            pallet2.PosY.Should().Be(1);
        }

        //dopisać jakieś hardkorowe przypadki, np. dla 3 palet.
    }
}
