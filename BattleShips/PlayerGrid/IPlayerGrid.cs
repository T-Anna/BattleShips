using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public interface IPlayerGrid
    {
        ShotResult GetShotResultForGivenCoordinates(Coordinates coordinates);
        void SetShotResultForGivenCoordinates(Coordinates shotCoordinates, ShotResult result);
        bool WasShotBefore(Coordinates shotCoordiantes);
    }
}