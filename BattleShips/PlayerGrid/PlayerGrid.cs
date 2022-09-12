using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public class PlayerGrid : IPlayerGrid
    {
        ShotResult[,] grid;

        public PlayerGrid()
        {
            grid = new ShotResult[10, 10];
        }

        public bool WasShotBefore(Coordinates shotCoordinates)
        {
            return grid[shotCoordinates.Row, shotCoordinates.Column] == ShotResult.Hit || grid[shotCoordinates.Row, shotCoordinates.Column] == ShotResult.Missed;
        }

        public void SetShotResultForGivenCoordinates(Coordinates shotCoordinates, ShotResult shotResult)
        {
            if (!WasShotBefore(shotCoordinates))
            {
                grid[shotCoordinates.Row, shotCoordinates.Column] = shotResult;
            }
        }

        public ShotResult GetShotResultForGivenCoordinates(Coordinates coordinates)
        {
            return grid[coordinates.Row, coordinates.Column];
        }
    }
}