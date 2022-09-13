using BattleShips;
using BattleShips.PlacingComputerShips;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BattleShipsTests.GameTests
{
    public class GameTests
    {
        private readonly Mock<IPlayerInteractions> mockPlayerInteractions;
        private readonly List<IShip> ships;
        private readonly Mock<IShip> mockShip1;
        private readonly Mock<IShip> mockShip2;
        private readonly Mock<IShipsPlacer> mockShipsPlacer;
        private readonly Mock<IShooter> mockShooter;
        private readonly Game game;

        public GameTests()
        {
            mockPlayerInteractions = new Mock<IPlayerInteractions>();
            ships = new List<IShip>();
            mockShip1 = new Mock<IShip>();
            mockShip2 = new Mock<IShip>();
            ships.Add(mockShip1.Object);
            ships.Add(mockShip2.Object);
            mockShipsPlacer = new Mock<IShipsPlacer>();
            mockShooter = new Mock<IShooter>();
            game = new Game(mockPlayerInteractions.Object, ships, mockShipsPlacer.Object, mockShooter.Object);
        }

        [Fact]
        public void ShipsShouldBePlacedOnce()
        {
            mockShip1.Setup(x => x.IsSunk()).Returns(true);
            mockShip2.Setup(x => x.IsSunk()).Returns(true);

            game.Play();
            mockShipsPlacer.Verify(x => x.PlaceShips(), Times.Once);
        }

        [Fact]
        public void ShootingShouldContinue_UntilAllShipsAreSunk()
        {
            mockShip1.SetupSequence(x => x.IsSunk()).Returns(false)
                .Returns(true)
                .Returns(true)
                .Returns(true);
            mockShip2.SetupSequence(x => x.IsSunk()).Returns(false)
                .Returns(true);
            game.Play();
            mockShooter.Verify(x => x.Shoot(), Times.Exactly(2));
        }

        [Fact]
        public void MessageWithShotResultShouldBeDisplayed_AfterEachShot()
        {
            var shot1 = new Shot(new Coordinates(1, 1));
            var shot2 = new Shot(new Coordinates(1, 2));

            mockShooter.SetupSequence(x => x.Shoot()).Returns(shot1)
                                                    .Returns(shot2);

            mockShip1.SetupSequence(x => x.IsSunk()).Returns(false)
                .Returns(false)
                .Returns(true);
            mockShip2.SetupSequence(x => x.IsSunk()).Returns(true);

            game.Play();
            mockShooter.Verify(x => x.Shoot(), Times.Exactly(2));
            mockPlayerInteractions.Verify(x => x.DisplayMessageWithShotResult(shot1), Times.Once);
            mockPlayerInteractions.Verify(x => x.DisplayMessageWithShotResult(shot2), Times.Once);
        }

        [Fact]
        public void PlayerGridShouldBeDisplayed_AfterEachShot()
        {
            mockShip1.SetupSequence(x => x.IsSunk()).Returns(false)
                .Returns(false)
                .Returns(true);
            mockShip2.SetupSequence(x => x.IsSunk()).Returns(true);

            game.Play();
            mockShooter.Verify(x => x.Shoot(), Times.Exactly(2));
            mockPlayerInteractions.Verify(x => x.DisplayPlayerGrid(), Times.Exactly(3));
        }

        [Fact]
        public void WelcomeMessageShouldBeDisplayedOnce()
        {
            mockShip1.Setup(x => x.IsSunk()).Returns(true);
            mockShip2.Setup(x => x.IsSunk()).Returns(true);

            game.Play();
            mockPlayerInteractions.Verify(x => x.DisplayWelcomeMessage(), Times.Once);
        }

        [Fact]
        public void PlayerGridShouldBeDisplayedOnce_RegardlessShooting()
        {
            mockShip1.Setup(x => x.IsSunk()).Returns(true);
            mockShip2.Setup(x => x.IsSunk()).Returns(true);

            game.Play();
            mockPlayerInteractions.Verify(x => x.DisplayPlayerGrid(), Times.Once);
            mockShooter.Verify(x => x.Shoot(), Times.Never);
        }

        [Fact]
        public void GameEndMessageShouldBeDisplayedOnce()
        {
            mockShip1.Setup(x => x.IsSunk()).Returns(true);
            mockShip2.Setup(x => x.IsSunk()).Returns(true);

            game.Play();
            mockPlayerInteractions.Verify(x => x.DisplayGameEndMessage(), Times.Once);
        }
    }
}
