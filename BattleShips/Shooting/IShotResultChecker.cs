using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public interface IShotResultChecker
    {
        ShotResult CheckShotResult(Coordinates shotCoordinates);
    }
}