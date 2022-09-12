using System.Collections.Generic;

namespace BattleShips
{
    public class ShipCoordinatesAvailabilityChecker : IShipCoordinatesAvailabilityChecker
    {
        private readonly IComputerGrid grid;

        public ShipCoordinatesAvailabilityChecker(IComputerGrid grid)
        {
            this.grid = grid;
        }

        public bool AreFieldsAvailableOnTheGrid(List<Coordinates> coordinates)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                if (!grid.GetField(coordinates[i]).IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
    }
}