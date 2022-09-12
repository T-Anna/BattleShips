using static BattleShips.ShotResultEnum;

namespace BattleShips
{
    public class Shot
    {
        public Shot(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public ShotResult ShotResult { get; set; }
        public IShip ShipHit { get; set; }
        public Coordinates Coordinates { get; }
    }
}