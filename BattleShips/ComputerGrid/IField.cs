namespace BattleShips
{
    public interface IField
    {
        IShip Ship { get; set; }

        bool IsEmpty();
    }
}