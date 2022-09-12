using static BattleShips.ShipTypeEnum;

namespace BattleShips
{
    public class Ship : IShip
    {
        public ShipType ShipType { get; }
        private int damage;

        public Ship(ShipType shipType)
        {
            ShipType = shipType;
        }

        public void TakeHit()
        {
            damage++;
        }

        public bool IsSunk()
        {
            return damage == GetSize();
        }

        public int GetSize()
        {
            return (int)ShipType;
        }
    }
}