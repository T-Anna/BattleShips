using BattleShips.PlacingComputerShips;
using System.Collections.Generic;

namespace BattleShips
{
    public class Game
    {
        private readonly IPlayerInteractions playerInteractions;
        private readonly List<IShip> shipsInGame;
        private readonly IShipsPlacer shipsPlacer;
        private readonly IShooter shooter;

        public Game(IPlayerInteractions playerInteractions, List<IShip> shipsInGame, IShipsPlacer shipsPlacer, IShooter shooter)
        {
            this.playerInteractions = playerInteractions;
            this.shipsInGame = shipsInGame;
            this.shipsPlacer = shipsPlacer;
            this.shooter = shooter;
        }

        public void Play()
        {
            shipsPlacer.PlaceShips();
            playerInteractions.DisplayWelcomeMessage();
            playerInteractions.DisplayPlayerGrid();

            while (!HasGameEnded())
            {
                var shot = shooter.Shoot();
                playerInteractions.DisplayMessageWithShotResult(shot); 
                playerInteractions.DisplayPlayerGrid();
            }
            playerInteractions.DisplayGameEndMessage();
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