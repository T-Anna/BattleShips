using System;
using System.Collections.Generic;
using static BattleShips.ShipOrientationEnum;

namespace BattleShips
{
    public class ShipCoordinatesFinder : IShipCoordinatesFinder
    {
        private readonly Random random;
        private readonly int gridSize = 10;

        public ShipCoordinatesFinder(Random random)
        {
            this.random = random;
        }

        public List<Coordinates> FindShipCoordinates(int shipSize)
        {
            var shipCoordinates = new List<Coordinates>();

            var shipOrientation = FindRandomShipOrientation();
            var initialCoordinates = FindRandomInitialShipCoordinates(shipOrientation, shipSize);

            shipCoordinates.Add(initialCoordinates);

            for (int i = 1; i < shipSize; i++)
            {
                if (shipOrientation == ShipOrientation.Horizontal)
                {
                    shipCoordinates.Add(new Coordinates(initialCoordinates.Row, initialCoordinates.Column + i));
                }
                else if (shipOrientation == ShipOrientation.Vertical)
                {
                    shipCoordinates.Add(new Coordinates(initialCoordinates.Row + i, initialCoordinates.Column));
                }
            }
            return shipCoordinates;
        }

        private ShipOrientation FindRandomShipOrientation()
        {
            return (ShipOrientation)random.Next(0, 2);
        }

        private Coordinates FindRandomInitialShipCoordinates(ShipOrientation shipOrientation, int shipSize)
        {
            int maxDistanceFromGridOriginToInitialShipPositionAlongAxisShipWillBePlacedOn = gridSize - shipSize;
            int row = -1;
            int column = -1;

            if (shipOrientation == ShipOrientation.Horizontal)
            {
                row = random.Next(0, gridSize);
                column = random.Next(0, maxDistanceFromGridOriginToInitialShipPositionAlongAxisShipWillBePlacedOn + 1);
            }
            else if (shipOrientation == ShipOrientation.Vertical)
            {
                row = random.Next(0, maxDistanceFromGridOriginToInitialShipPositionAlongAxisShipWillBePlacedOn + 1);
                column = random.Next(0, gridSize);
            }

            return new Coordinates(row, column);
        }
    }
}