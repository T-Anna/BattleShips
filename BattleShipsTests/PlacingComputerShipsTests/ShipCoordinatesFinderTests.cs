using System;
using Moq;
using Xunit;
using BattleShips;
using static BattleShips.ShipOrientationEnum;
using static BattleShips.ShipTypeEnum;

namespace BattleShipsTests.PlacingComputerShipsTests
{

    public class ShipCoordinatesFinderTests
    {
        private readonly int gridSize = 10;

        [Theory]
        [InlineData((int)ShipType.Destroyer)]
        [InlineData((int)ShipType.BattleShip)]
        public void ShipCoordinatesShouldHaveLengthOfGivenShipSize(int shipSize)
        {
            var randomMock = new Mock<Random>();

            var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);
            var coordinates = shipCoordinatesFinder.FindShipCoordinates(shipSize);
            Assert.Equal(shipSize, coordinates.Count);
        }

        [Theory]
        [InlineData((int)ShipType.Destroyer)]
        [InlineData((int)ShipType.BattleShip)]
        public void InitialShipCoordinatesShouldBeRandomlyChosen(int shipSize)
        {
            var randomMock = new Mock<Random>();
            var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);

            shipCoordinatesFinder.FindShipCoordinates(shipSize);
            randomMock.Verify(x => x.Next(0, It.IsInRange(gridSize - shipSize, gridSize, Moq.Range.Inclusive)), Times.Exactly(2));
        }

        [Theory]
        [InlineData((int)ShipType.Destroyer)]
        [InlineData((int)ShipType.BattleShip)]
        public void InitialShipCoordinatesShouldBeChosenSoTheShipDoesNotExceedGridBorder_VerticalShipOrientation(int shipSize)
        {
            int expectedInitialRow = 1;
            int expectecInitialColumn = 9;

            var randomMock = new Mock<Random>();
            randomMock.Setup(x => x.Next(0, 2)).Returns((int)ShipOrientation.Vertical);
            randomMock.Setup(x => x.Next(0, gridSize - shipSize + 1)).Returns(expectedInitialRow);
            randomMock.Setup(x => x.Next(0, gridSize)).Returns(expectecInitialColumn);

            var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);
            var shipCoordinates = shipCoordinatesFinder.FindShipCoordinates(shipSize);

            randomMock.Verify(x => x.Next(0, gridSize - shipSize + 1), Times.Once);
            randomMock.Verify(x => x.Next(0, gridSize), Times.Once);

            Assert.Equal(expectedInitialRow, shipCoordinates[0].Row);
            Assert.Equal(expectecInitialColumn, shipCoordinates[0].Column);
        }

         [Theory]
         [InlineData((int)ShipType.Destroyer)]
         [InlineData((int)ShipType.BattleShip)]
         public void InitialShipCoordinatesShouldBeChosenSoTheShipDoesNotExceedGridBorder_HorizontalShipOrientation(int shipSize)
         {
            int expectedInitialRow = 9;
            int expectecInitialColumn = 1;

            var randomMock = new Mock<Random>();
            randomMock.Setup(x => x.Next(0, 2)).Returns((int)ShipOrientation.Horizontal);
            randomMock.Setup(x => x.Next(0, gridSize)).Returns(expectedInitialRow);
            randomMock.Setup(x => x.Next(0, gridSize - shipSize + 1)).Returns(expectecInitialColumn);

            var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);
            var shipCoordinates = shipCoordinatesFinder.FindShipCoordinates(shipSize);

            randomMock.Verify(x => x.Next(0, gridSize - shipSize + 1), Times.Once);
            randomMock.Verify(x => x.Next(0, gridSize), Times.Once);

            Assert.Equal(expectedInitialRow, shipCoordinates[0].Row);
            Assert.Equal(expectecInitialColumn, shipCoordinates[0].Column);
        }
        
         [Theory]
         [InlineData((int)ShipType.Destroyer)]
         [InlineData((int)ShipType.BattleShip)]
         public void ShipOrientationShouldBeRandomlyChosen(int shipSize)
         {
             var randomMock = new Mock<Random>();
             var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);
             shipCoordinatesFinder.FindShipCoordinates(shipSize);
             randomMock.Verify(x => x.Next(0, 2), Times.Once);
         }
        
         [Theory]
         [InlineData((int)ShipType.Destroyer)]
         [InlineData((int)ShipType.BattleShip)]
         public void ShipCoordinatesShouldBeAdjacentToEachOther_VerticalShipOrientation(int shipSize)
         {
             int initialCoordinatesRow = 1;
             int initialCoordinatesColumn = 9;

             var randomMock = new Mock<Random>();
             randomMock.Setup(x => x.Next(0, 2)).Returns((int)ShipOrientation.Vertical);
             randomMock.Setup(x => x.Next(0, 10 - shipSize + 1)).Returns(initialCoordinatesRow);
             randomMock.Setup(x => x.Next(0, 10)).Returns(initialCoordinatesColumn);

             var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);

             var coordinates = shipCoordinatesFinder.FindShipCoordinates(shipSize);

             for (int i = 0; i < shipSize; i++)
             {
                 Assert.Equal(initialCoordinatesRow + i, coordinates[i].Row);
                 Assert.Equal(initialCoordinatesColumn, coordinates[i].Column);
             }
         }

        [Theory]
        [InlineData((int)ShipType.Destroyer)]
        [InlineData((int)ShipType.BattleShip)]
        public void ShipCoordinatesShouldBeAdjacentToEachOther_HorizontalShipOrientation(int shipSize)
        {
            int initialCoordinatesRow = 9;
            int initialCoordinatesColumn = 1;

            var randomMock = new Mock<Random>();
            randomMock.Setup(x => x.Next(0, 2)).Returns((int)ShipOrientation.Horizontal);
            randomMock.Setup(x => x.Next(0, 10)).Returns(initialCoordinatesRow);
            randomMock.Setup(x => x.Next(0, 10 - shipSize + 1)).Returns(initialCoordinatesColumn);

            var shipCoordinatesFinder = new ShipCoordinatesFinder(randomMock.Object);

            var coordinates = shipCoordinatesFinder.FindShipCoordinates(shipSize);

            for (int i = 0; i < shipSize; i++)
            {
                Assert.Equal(initialCoordinatesRow, coordinates[i].Row);
                Assert.Equal(initialCoordinatesColumn + i, coordinates[i].Column);
            }
        }

    }
}
