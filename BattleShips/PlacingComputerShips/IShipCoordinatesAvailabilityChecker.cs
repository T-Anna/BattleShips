using System.Collections.Generic;

namespace BattleShips
{
    public interface IShipCoordinatesAvailabilityChecker
    {
        bool AreFieldsAvailableOnTheGrid(List<Coordinates> coordinates);
    }
}