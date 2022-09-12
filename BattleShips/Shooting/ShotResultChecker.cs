using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public class ShotResultChecker : IShotResultChecker
    {
        private readonly IComputerGrid computerGrid;

        public ShotResultChecker(IComputerGrid computerGrid)
        {
            this.computerGrid = computerGrid;
        }

        public ShotResult CheckShotResult(Coordinates shotCoordinates)
        {
            if (computerGrid.GetField(shotCoordinates).IsEmpty())
            {
                return ShotResult.Missed;
            }
            else
            {
                return ShotResult.Hit;
            }
        }
    }
}
