using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public class Shooter : IShooter
    {
        private readonly IPlayerGrid playerGrid;
        private readonly IShotResultChecker shotResultChecker;
        private readonly IShotCoordinatesTaker shotCoordinatesTaker;
        private readonly IComputerGrid computerGrid;


        public Shooter(IPlayerGrid playerGrid, IShotResultChecker shotResultChecker, IShotCoordinatesTaker shotCoordinatesTaker,  IComputerGrid computerGrid)
        {
            this.playerGrid = playerGrid;
            this.shotResultChecker = shotResultChecker;
            this.shotCoordinatesTaker = shotCoordinatesTaker;
            this.computerGrid = computerGrid;
        }

        public Shot Shoot()
        {
            var shotCoordinates = shotCoordinatesTaker.DetermineShotCoordinates();
            var shot = new Shot(shotCoordinates);
            shot.ShotResult = shotResultChecker.CheckShotResult(shotCoordinates);
            if(shot.ShotResult == ShotResult.Hit)
            {
                var ship = computerGrid.GetField(shotCoordinates).Ship;
                shot.ShipHit = ship;
                ship.TakeHit();
            }
            playerGrid.SetShotResultForGivenCoordinates(shot.Coordinates, shot.ShotResult);
            return shot;
        }
    }
}
