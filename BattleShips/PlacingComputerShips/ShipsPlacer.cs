using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.PlacingComputerShips
{
    public class ShipsPlacer : IShipsPlacer
    {
        private readonly IShipCoordinatesFinder shipCoordinatesFinder;
        private readonly IShipCoordinatesAvailabilityChecker shipCoordinatesAvailabilityChecker;
        private readonly IShipToFieldsAssigner shipToFieldsAssigner;
        private readonly List<IShip> shipsToBePlaced;

        public ShipsPlacer(
            IShipCoordinatesFinder shipCoordinatesFinder,
            IShipCoordinatesAvailabilityChecker shipCoordinatesAvailabilityChecker,
            IShipToFieldsAssigner shipToFieldsAssigner,
            List<IShip> shipsToBePlaced
            )
        {
            this.shipCoordinatesFinder = shipCoordinatesFinder;
            this.shipCoordinatesAvailabilityChecker = shipCoordinatesAvailabilityChecker;
            this.shipToFieldsAssigner = shipToFieldsAssigner;
            this.shipsToBePlaced = shipsToBePlaced;
        }

        public void PlaceShips()
        {
            foreach (var ship in shipsToBePlaced)
            {
                int tryCount = 50;
                bool wasShipPlaced = false; ;
                do
                {
                    var shipCoordinates = shipCoordinatesFinder.FindShipCoordinates(ship.GetSize());
                    if (shipCoordinatesAvailabilityChecker.AreFieldsAvailableOnTheGrid(shipCoordinates))
                    {
                        shipToFieldsAssigner.AssignShipToGivenFieldsCoordinatesOnTheGrid(ship, shipCoordinates);
                        wasShipPlaced = true;
                    }
                    tryCount--;
                }
                while (wasShipPlaced == false && tryCount > 0);
            }
        }
    }
}
