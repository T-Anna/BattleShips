using System.Collections.Generic;

namespace BattleShips
{
    public interface IShipToFieldsAssigner
    {
        void AssignShipToFieldsForGivenCoordinatesofTheGrid(IShip shipToBePlaced, List<Coordinates> coordinates);
    }
}