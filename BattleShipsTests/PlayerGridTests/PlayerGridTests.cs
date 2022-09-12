using BattleShips;
using Xunit;
using static BattleShips.ShotResultEnum;

namespace BattleShipsTests.PlayerGridTests
{
    public class PlayerGridTests
    {
        [Fact]
        public void WasShotBefore_ReturnsFalseWhenPlayerDidntEnterGivenCoordinatesBefore()
        {
            var playerGrid = new PlayerGrid();
            var result = playerGrid.WasShotBefore(new Coordinates(0, 0));
            Assert.False(result);
        }

        [Theory]
        [InlineData(ShotResult.Missed)]
        [InlineData(ShotResult.Hit)]

        public void WasShotBefore_ReturnsTrueWhenPlayerEnteredGivenCoordinatesBefore(ShotResult previousShotResult)
        {
            var coordinates = new Coordinates(0, 0);
            var playerGrid = new PlayerGrid();
            playerGrid.SetShotResultForGivenCoordinates(coordinates, previousShotResult);
            var result = playerGrid.WasShotBefore(coordinates);
            Assert.True(result);
        }

        [Theory]
        [InlineData(ShotResult.Missed)]
        [InlineData(ShotResult.Hit)]
        public void ShotResultForGivenCoordinatesIsSet_WhenItWasNotSetBefore(ShotResult shotResult)
        {
            var coordinates = new Coordinates(0, 0);
            var playerGrid = new PlayerGrid();
            playerGrid.SetShotResultForGivenCoordinates(coordinates, shotResult);
            var shotResultAfterSetForGivenCoordinates = playerGrid.GetShotResultForGivenCoordinates(coordinates);
            Assert.Equal(shotResult, shotResultAfterSetForGivenCoordinates);
        }

        [Fact]
        public void GridStateForGivenCoordinatesIsNotSet_WhenItWasSetBefore()
        {
            var coordinates = new Coordinates(0, 0);
            var playerGrid = new PlayerGrid();
            playerGrid.SetShotResultForGivenCoordinates(coordinates, ShotResult.Missed);
            playerGrid.SetShotResultForGivenCoordinates(coordinates, ShotResult.Hit);

            var shotResult = playerGrid.GetShotResultForGivenCoordinates(coordinates);
            Assert.Equal(ShotResult.Missed, shotResult);
        }
    }
}
