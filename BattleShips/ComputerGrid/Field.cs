namespace BattleShips
{
    public class Field : IField
    {
        public IShip Ship { get; set; }

        public bool IsEmpty()
        {
            return Ship == null;
        }
    }
}