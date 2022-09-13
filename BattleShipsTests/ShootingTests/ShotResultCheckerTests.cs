using BattleShips;
using Moq;
using Xunit;
using static BattleShips.ShotResultEnum;

namespace BattleShipsTests.ShootingTests
{
    public class ShotResultCheckerTests
    {
        [Fact]
        public void CheckShotResultReturnsMissedShotResult_WhenFieldForGivenCoordinatesIsEmpty()
        {
            var coordinates = new Coordinates(5, 5);
            var mockComputerGrid = new Mock<IComputerGrid>();
            mockComputerGrid.Setup(x => x.GetField(coordinates).IsEmpty()).Returns(true);
            var shotResultChecker = new ShotResultChecker(mockComputerGrid.Object);
            var shotResult = shotResultChecker.CheckShotResult(coordinates);
            Assert.Equal(ShotResult.Missed, shotResult);
        }

        [Fact]
        public void CheckShotResultReturnsHitShotResult_WhenFieldForGivenCoordinatesIsNotEmpty()
        {
            var coordinates = new Coordinates(5, 5);
            var mockComputerGrid = new Mock<IComputerGrid>();
            mockComputerGrid.Setup(x => x.GetField(coordinates).IsEmpty()).Returns(false);
            var shotResultChecker = new ShotResultChecker(mockComputerGrid.Object);
            var shotResult = shotResultChecker.CheckShotResult(coordinates);
            Assert.Equal(ShotResult.Hit, shotResult);
        }
    }
}
