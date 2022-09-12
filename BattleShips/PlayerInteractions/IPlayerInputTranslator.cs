namespace BattleShips
{
    public interface IPlayerInputTranslator
    {
        Coordinates TranslateCoordinates(string input);
    }
}