using System;
using System.Collections.Generic;

namespace BattleShips
{
    public class ShipToFieldsAssigner : IShipToFieldsAssigner
    {
        private readonly IComputerGrid grid;

        public ShipToFieldsAssigner(IComputerGrid grid)
        {
            this.grid = grid;
        }

        public void AssignShipToFieldsForGivenCoordinatesofTheGrid(IShip shipToBePlaced, List<Coordinates> coordinates)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                var field = grid.GetField(coordinates[i]);
                field.Ship = shipToBePlaced;
            }
        }
    }
}