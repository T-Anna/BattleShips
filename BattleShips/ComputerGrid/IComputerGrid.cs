namespace BattleShips
{
    public interface IComputerGrid
    {
        IField GetField(Coordinates coordinates);
    }
}