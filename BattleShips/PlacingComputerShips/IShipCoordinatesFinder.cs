using System.Collections.Generic;

namespace BattleShips
{
    public interface IShipCoordinatesFinder
    {
        List<Coordinates> FindShipCoordinates(int shipSize);
    }
}