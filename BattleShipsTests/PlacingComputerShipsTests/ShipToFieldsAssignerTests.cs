using BattleShips;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BattleShipsTests.PlacingComputerShipsTests
{
    public class ShipToFieldsAssignerTests
    {
        [Fact]
        public void ShouldAssignShipToFieldsForGivenCoordinatesOfTheGrid()
        {
            var coordinates = new List<Coordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2), new Coordinates(0, 3) };

            var mockShip = new Mock<IShip>();
            var mockGrid = new Mock<IComputerGrid>();
            var mockFields = new List<Mock<IField>>();

            for (int i = 0; i < coordinates.Count; i++)
            {
                var newMockField = new Mock<IField>();
                mockFields.Add(newMockField);
                mockGrid.Setup(x => x.GetField(coordinates[i])).Returns(newMockField.Object);
            }

            var shipPlacer = new ShipToFieldsAssigner(mockGrid.Object);
            shipPlacer.AssignShipToFieldsForGivenCoordinatesofTheGrid(mockShip.Object, coordinates);
            foreach (var field in mockFields)
            {
                field.VerifySet(x => x.Ship = mockShip.Object, Times.Once);
            }
        }
    }
}
