using System.Collections.Generic;

namespace BattleShips
{
    public interface IShipToFieldsAssigner
    {
        void AssignShipToGivenFieldsCoordinatesOnTheGrid(IShip shipToBePlaced, List<Coordinates> coordinates);
    }
}