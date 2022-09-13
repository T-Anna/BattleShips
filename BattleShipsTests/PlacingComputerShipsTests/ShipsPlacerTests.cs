using BattleShips;
using BattleShips.PlacingComputerShips;
using Moq;
using System.Collections.Generic;
using Xunit;
using static BattleShips.ShipTypeEnum;

namespace BattleShipsTests.PlacingComputerShipsTests
{
    public class ShipsPlacerTests
    {
        private readonly List<Coordinates> destroyerCoordinates;
        private readonly List<Coordinates> battleShipCoordinates;
        private readonly Mock<IShip> mockShipDestroyer;
        private readonly Mock<IShip> mockShipBattleShip;
        private readonly Mock<IShipCoordinatesFinder> mockShipCoordinatesFinder;
        private readonly Mock<IShipCoordinatesAvailabilityChecker> mockShipCoordinatesAvailabilityChecker;
        private readonly Mock<IShipToFieldsAssigner> mockShipToFieldsAssigner;
        private readonly List<IShip> shipsToBePlaced;
        private readonly ShipsPlacer shipPlacer;

        public ShipsPlacerTests()
        {
            mockShipCoordinatesFinder = new Mock<IShipCoordinatesFinder>();
            mockShipCoordinatesAvailabilityChecker = new Mock<IShipCoordinatesAvailabilityChecker>();
            mockShipToFieldsAssigner = new Mock<IShipToFieldsAssigner>();

            destroyerCoordinates = new List<Coordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2), new Coordinates(0, 3) };
            battleShipCoordinates = new List<Coordinates> { new Coordinates(1, 0), new Coordinates(1, 1), new Coordinates(1, 2), new Coordinates(1, 3), new Coordinates(1, 4) };

            mockShipDestroyer = new Mock<IShip>();
            mockShipBattleShip = new Mock<IShip>();
            mockShipDestroyer.Setup(x => x.GetSize()).Returns((int)ShipType.Destroyer);
            mockShipBattleShip.Setup(x => x.GetSize()).Returns((int)ShipType.BattleShip);
            
            shipsToBePlaced = new List<IShip>();
            shipsToBePlaced.Add(mockShipDestroyer.Object);
            shipsToBePlaced.Add(mockShipBattleShip.Object);
            
            shipPlacer = new ShipsPlacer(mockShipCoordinatesFinder.Object, mockShipCoordinatesAvailabilityChecker.Object, mockShipToFieldsAssigner.Object, shipsToBePlaced);
        }

        [Fact]
        public void ShouldDetermineCoordinatesOfTheGridShipsWillBePlacedOn()
        {
            shipPlacer.PlaceShips();
            mockShipCoordinatesFinder.Verify(x => x.FindShipCoordinates((int)ShipType.Destroyer), Times.AtLeastOnce);
            mockShipCoordinatesFinder.Verify(x => x.FindShipCoordinates((int)ShipType.BattleShip), Times.AtLeastOnce);
        }

       [Fact]
        public void ShouldPlaceShipsOnTheGivenFieldsCoordinatesOfTheGrid_WhenFieldsAreAvailable()
        {
            mockShipCoordinatesFinder.Setup(x => x.FindShipCoordinates((int)ShipType.Destroyer)).Returns(destroyerCoordinates);
            mockShipCoordinatesAvailabilityChecker.Setup(x => x.AreFieldsAvailableOnTheGrid(destroyerCoordinates)).Returns(true);
            mockShipCoordinatesFinder.Setup(x => x.FindShipCoordinates((int)ShipType.BattleShip)).Returns(battleShipCoordinates);
            mockShipCoordinatesAvailabilityChecker.Setup(x => x.AreFieldsAvailableOnTheGrid(battleShipCoordinates)).Returns(true);

            shipPlacer.PlaceShips();

            mockShipToFieldsAssigner.Verify(x => x.AssignShipToGivenFieldsCoordinatesOnTheGrid(mockShipDestroyer.Object, destroyerCoordinates), Times.Once);
            mockShipToFieldsAssigner.Verify(x => x.AssignShipToGivenFieldsCoordinatesOnTheGrid(mockShipBattleShip.Object, battleShipCoordinates), Times.Once);
        }
        
        [Fact]
        public void ShouldNotPlaceShipOnTheGivenFieldsCoordinatesOfTheGrid_WhenFieldsAreNotAvailable()
        {
            mockShipCoordinatesFinder.Setup(x => x.FindShipCoordinates((int)ShipType.Destroyer)).Returns(destroyerCoordinates);
            mockShipCoordinatesAvailabilityChecker.Setup(x => x.AreFieldsAvailableOnTheGrid(destroyerCoordinates)).Returns(false);
            mockShipCoordinatesFinder.Setup(x => x.FindShipCoordinates((int)ShipType.BattleShip)).Returns(battleShipCoordinates);
            mockShipCoordinatesAvailabilityChecker.Setup(x => x.AreFieldsAvailableOnTheGrid(battleShipCoordinates)).Returns(false);

            shipPlacer.PlaceShips();

            mockShipToFieldsAssigner.Verify(x => x.AssignShipToGivenFieldsCoordinatesOnTheGrid(mockShipDestroyer.Object, destroyerCoordinates), Times.Never);
            mockShipToFieldsAssigner.Verify(x => x.AssignShipToGivenFieldsCoordinatesOnTheGrid(mockShipBattleShip.Object, battleShipCoordinates), Times.Never);
        }

        [Fact]
        public void ShouldKeepLookingForShipCoordinatesUpTo50times_WhenFoundCoordinatesAreNotAvaialbe()
        {
            mockShipCoordinatesAvailabilityChecker.Setup(x => x.AreFieldsAvailableOnTheGrid(It.IsAny<List<Coordinates>>())).Returns(false);

            shipPlacer.PlaceShips();
            mockShipCoordinatesFinder.Verify(x => x.FindShipCoordinates((int)ShipType.Destroyer), Times.Exactly(50));
            mockShipCoordinatesFinder.Verify(x => x.FindShipCoordinates((int)ShipType.BattleShip), Times.Exactly(50));
        }
    }
}
