using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Services
{
    public class ConveyorMovementServiceTests
    {
        //No pallets and no conveyors cases
        [Fact]
        public void HandleConveyorMovement_NoPallets_NoExceptionsThrown()
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
            var service = new ConveyingService(new List<Conveyor> { conveyor }, new List<Pallet>());

            //act
            Action sut = () => service.HandleConveyorMovement();

            //assert
            sut.Should().NotThrow();
        }

        [Fact]
        public void HandleConveyorMovement_PalletsNull_NoExceptionsThrown()
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
            var service = new ConveyingService(new List<Conveyor> { conveyor }, null);

            //act
            Action sut = () => service.HandleConveyorMovement();

            //assert
            sut.Should().NotThrow();
        }

        [Fact]
        public void HandleConveyorMovement_NoConveyors_NoExceptionsThrown()
        {
            //arrange
            var pallet = new Pallet(0, 0);
            var service = new ConveyingService(new List<Conveyor>(), new List<Pallet> {pallet});

            //act
            Action sut = () => service.HandleConveyorMovement();

            //assert
            sut.Should().NotThrow();
        }

        [Fact]
        public void HandleConveyorMovement_ConveyorsNull_NoExceptionsThrown()
        {
            //arrange
            var pallet = new Pallet(0, 0);
            var service = new ConveyingService(null, new List<Pallet> {pallet});

            //act
            Action sut = () => service.HandleConveyorMovement();

            //assert
            sut.Should().NotThrow();
        }

        [Fact]
        public void HandleConveyorMovement_PalletOnConveyor_PalletShouldMove()
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
            var sut = new ConveyingService(new List<Conveyor> {conveyor}, new List<Pallet> {pallet });
            
            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().HaveCount(1);
            sut.ModifiedPallets.FirstOrDefault().Should().Be(pallet);
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public void HandleConveyorMovement_PalletsOnConveyor_PalletsShouldMove()
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
            var sut = new ConveyingService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet,pallet2 });

            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().HaveCount(2);
            sut.ModifiedPallets.Any(x => x == pallet).Should().BeTrue();
            sut.ModifiedPallets.Any(x => x == pallet2).Should().BeTrue();
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(2);
            pallet2.PosY.Should().Be(0);
        }

        [Fact]
        public void HandleConveyorMovement_PalletNearConveyor_PalletShouldStay()
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
            var sut = new ConveyingService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet});

            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().BeEmpty();
            pallet.PosX.Should().Be(6);
            pallet.PosY.Should().Be(0);
        }

        [Fact]
        public void HandleConveyorMovement_PalletBlocksPallet_PalletsShouldStay()
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
            var sut = new ConveyingService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet,pallet2 });
            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().BeEmpty();
            pallet.PosX.Should().Be(4);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(5);
            pallet2.PosY.Should().Be(0);
        }

        [Fact]
        public void HandleConveyorMovement_TwoPalletsFallToTheSamePlace_OnePalletShouldFall()
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

            var sut = new ConveyingService(new List<Conveyor> { conveyor, conveyor2 }, pallets);
            
            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Count.Should().Be(1);
            pallets.Count.Should().Be(2);
            pallets.Where(x => x.PosX == 3).Should().HaveCount(1);
        }

        [Fact]
        public void HandleConveyorMovement_CollisionWhileMovingBetweenConveyors_OnePalletShouldWait()
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

            var sut = new ConveyingService(new List<Conveyor> { conveyor, conveyor2 }, new List<Pallet> { pallet, pallet2 });

            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.FirstOrDefault().Should().Be(pallet);
            pallet.PosX.Should().Be(1);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(1);
            pallet2.PosY.Should().Be(1);
        }

        [Fact]
        public void HandleConveyorMovement_TwoPalletsFallToTheSameLockedPlace_AllPalletsShouldWait()
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
            var pallet3 = new Pallet(3, 0);
            var pallets = new List<Pallet> { pallet, pallet2, pallet3 };

            var sut = new ConveyingService(new List<Conveyor> { conveyor, conveyor2 }, pallets);
            
            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().BeEmpty();
            pallet.PosX.Should().Be(2);
            pallet.PosY.Should().Be(0);
            pallet2.PosX.Should().Be(4);
            pallet2.PosY.Should().Be(0);
            pallet3.PosX.Should().Be(3);
            pallet3.PosY.Should().Be(0);
        }

        [Fact]
        public void HandleConveyorMovement_BlockedPalletsOnConveyor_PalletsShouldStay()
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
            var pallet2 = new Pallet(3, 0);
            var pallet3 = new Pallet(2, 0);
            var pallet4 = new Pallet(5, 0);
            var sut = new ConveyingService(new List<Conveyor> { conveyor }, new List<Pallet> { pallet, pallet2,pallet3,pallet4 });

            //act
            sut.HandleConveyorMovement();

            //assert
            sut.ModifiedPallets.Should().BeEmpty();
            pallet.PosX.Should().Be(4);
            pallet2.PosX.Should().Be(3);
            pallet3.PosX.Should().Be(2);
            pallet4.PosX.Should().Be(5);
        }
    }
}
