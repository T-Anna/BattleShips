using BattleShips;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BattleShipsTests.PlacingComputerShipsTests
{
    public class ShipCoordinatesAvailabilityCheckerTests
    {
        [Fact]
        public void ShouldReturnTrueIfAllFieldsForGivenCoordinatesAreEmpty()
        {
            var coordinates = new List<Coordinates> { new Coordinates(0,0), new Coordinates(0, 1), new Coordinates(0, 2), new Coordinates(0, 3) };

            var mockGrid = new Mock<IComputerGrid>();
            for (int i = 0; i < 4; i++)
            {
                mockGrid.Setup(x => x.GetField(coordinates[i]).IsEmpty()).Returns(true);
            }
            var availabilityChecker = new ShipCoordinatesAvailabilityChecker(mockGrid.Object);
            var result = availabilityChecker.AreFieldsAvailableOnTheGrid(coordinates);
            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseIfAtLeastOneFieldForGivenCoordinatesIsNotEmpty()
        {
            var coordinates = new List<Coordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2), new Coordinates(0, 3) };
            var mockGrid = new Mock<IComputerGrid>();
            mockGrid.Setup(x => x.GetField(coordinates[0]).IsEmpty()).Returns(false);
            
            var availabilityChecker = new ShipCoordinatesAvailabilityChecker(mockGrid.Object);
            var result = availabilityChecker.AreFieldsAvailableOnTheGrid(coordinates);
            Assert.False(result);
        }
    }
}
