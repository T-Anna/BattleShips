using BattleShips.PlacingComputerShips;
using System.Collections.Generic;

namespace BattleShips
{
    public class Game
    {
        private readonly IPlayerInteractions userInteractions;
        private readonly List<IShip> shipsInGame;
        private readonly IShipsPlacer shipsPlacer;
        private readonly IShooter shooter;

        public Game(IPlayerInteractions userInteractions, List<IShip> shipsInGame, IShipsPlacer shipsPlacer, IShooter shooter)
        {
            this.userInteractions = userInteractions;
            this.shipsInGame = shipsInGame;
            this.shipsPlacer = shipsPlacer;
            this.shooter = shooter;
        }

        public void Play()
        {
            shipsPlacer.PlaceShips();
            userInteractions.DisplayWelcomeMessage();
            userInteractions.DisplayPlayerGrid();

            while (!HasGameEnded())
            {
                var shot = shooter.Shoot();
                userInteractions.DisplayMessageWithShotResult(shot); 
                userInteractions.DisplayPlayerGrid();
            }
            userInteractions.DisplayGameEndMessage();
        }

        private bool HasGameEnded()
        {
            foreach (var ship in shipsInGame)
            {
                if (!ship.IsSunk())
                {
                    return false;
                }
            }
            return true;
        }
    }
}