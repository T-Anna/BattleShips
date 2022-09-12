using BattleShips.PlacingComputerShips;
using System;
using System.Collections.Generic;
using static BattleShips.ShipTypeEnum;

namespace BattleShips
{
    public static class GameInitializer
    {
        public static Game CreateGame()
        {
            var computerGrid = CreateComputerGrid();
            var ships = CreateShips();
            var shipsPlacer = CreateShipsPlacer(computerGrid, ships);
            var playerGrid = new PlayerGrid();
            var userInteractions = new PlayerInteractions(new PlayerInputTranslator(), playerGrid);
            var shooter = CreateShooter(computerGrid, playerGrid, userInteractions);

            return new Game(userInteractions, ships, shipsPlacer, shooter);
        }

        private static Shooter CreateShooter(IComputerGrid computerGrid, PlayerGrid playerGrid, PlayerInteractions userInteractions)
        {
            var shotCoordinatesTaker = new ShotCoordinatesTaker(userInteractions, playerGrid);
            var shotResultChecker = new ShotResultChecker(computerGrid);
            return new Shooter(playerGrid, shotResultChecker, shotCoordinatesTaker, computerGrid);
        }

        private static ShipsPlacer CreateShipsPlacer(IComputerGrid computerGrid, List<IShip> shipsToBePlaced)
        {
            Random rand = new Random();
            var shipsPlacer = new ShipsPlacer(
                new ShipCoordinatesFinder(rand),
                new ShipCoordinatesAvailabilityChecker(computerGrid),
                new ShipToFieldsAssigner(computerGrid),
                shipsToBePlaced);
            return shipsPlacer;
        }

        private static List<IShip> CreateShips()
        {
            return new List<IShip>()
                {new Ship(ShipType.BattleShip),
                 new Ship(ShipType.Destroyer),
                 new Ship(ShipType.Destroyer)
            };
        }

        private static IComputerGrid CreateComputerGrid()
        {
            IField[,] fields = new IField[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    fields[i, j] = new Field();
                }
            }
            return new ComputerGrid(fields);
        }
    }
}
