using static BattleShips.ShipTypeEnum;

namespace BattleShips
{
    public interface IShip
    {
        ShipType ShipType { get; }
        
        void TakeHit();
        bool IsSunk();
        int GetSize();
    }
}