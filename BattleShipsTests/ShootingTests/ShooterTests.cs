using BattleShips;
using Moq;
using Xunit;
using static BattleShips.ShotResultEnum;

namespace BattleShipsTests.ShootingTests
{
    public class ShooterTests
    {
        private readonly Mock<IPlayerGrid> mockPlayerGrid;
        private readonly Mock<IComputerGrid> mockComputerGrid;
        private readonly Mock<IShotResultChecker> mockShotResultChecker;
        private readonly Mock<IShotCoordinatesTaker> mockShotCoordinatesTaker;
        private readonly Mock<IShip> mockShip;
        private readonly Shooter shooter;

        public ShooterTests()
        {
            mockPlayerGrid = new Mock<IPlayerGrid>();
            mockComputerGrid = new Mock<IComputerGrid>();
            mockShotResultChecker = new Mock<IShotResultChecker>();
            mockShotCoordinatesTaker = new Mock<IShotCoordinatesTaker>();

            shooter = new Shooter(mockPlayerGrid.Object, mockShotResultChecker.Object, mockShotCoordinatesTaker.Object, mockComputerGrid.Object);
            mockShip = new Mock<IShip>();
        }

        [Fact]
        public void ShootSholdReturnShot()
        {
            var shot = shooter.Shoot();
            Assert.NotNull(shot);
        }

        [Fact]
        public void AfterShootingShotShouldHaveCoordinatesSet()
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            var shot = shooter.Shoot();
            Assert.Equal(shotCoordinates, shot.Coordinates);
        }

        [Theory]
        [InlineData(ShotResult.Missed)]
        [InlineData(ShotResult.Hit)]

        public void Shoot_ShouldSetShotResultOnTheShot(ShotResult expectedShotResult)
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            mockShotResultChecker.Setup(x => x.CheckShotResult(shotCoordinates)).Returns(expectedShotResult);
            mockComputerGrid.Setup(x => x.GetField(shotCoordinates).Ship).Returns(mockShip.Object);
            var shot = shooter.Shoot();
            Assert.Equal(expectedShotResult, shot.ShotResult);
        }

        [Fact]
        public void WhenShotResultIsHit_ShipShouldBeHit()
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            mockShotResultChecker.Setup(x => x.CheckShotResult(shotCoordinates)).Returns(ShotResult.Hit);
            mockComputerGrid.Setup(x => x.GetField(shotCoordinates).Ship).Returns(mockShip.Object);
            shooter.Shoot();
            mockShip.Verify(x => x.TakeHit(), Times.Once);
        }
        
        [Fact]
        public void WhenShotResultIsMissed_ShipShouldNotBeHit()
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            mockShotResultChecker.Setup(x => x.CheckShotResult(shotCoordinates)).Returns(ShotResult.Missed);
            shooter.Shoot();
            mockShip.Verify(x => x.TakeHit(), Times.Never);
        }

        [Fact]
        public void WhenShotResultIsHit_ShipHitIsSetOnShot()
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            mockShotResultChecker.Setup(x => x.CheckShotResult(shotCoordinates)).Returns(ShotResult.Hit);
            mockComputerGrid.Setup(x => x.GetField(shotCoordinates).Ship).Returns(mockShip.Object);
            var shot = shooter.Shoot();
            Assert.Equal(mockShip.Object, shot.ShipHit);
        }

        [Fact]
        public void WhenShotResultIsMissed_ShipHitOnShotIsNull()
        {
            var shotCoordinates = new Coordinates(1, 1);
            mockShotCoordinatesTaker.Setup(x => x.DetermineShotCoordinates()).Returns(shotCoordinates);
            mockShotResultChecker.Setup(x => x.CheckShotResult(shotCoordinates)).Returns(ShotResult.Missed);
            var shot = shooter.Shoot();
            
            Assert.Null(shot.ShipHit);
        }

        [Fact]
        public void Shoot_ShouldSetShotResultOnPlayerGrid()
        {
            var shot = shooter.Shoot();
            mockPlayerGrid.Verify(x => x.SetShotResultForGivenCoordinates(shot.Coordinates, shot.ShotResult));
        }
    }
}
