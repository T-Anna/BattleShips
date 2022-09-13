namespace BattleShips
{
    public interface IPlayerInteractions
    {
        void DisplayGameEndMessage();
        void DisplayMessageWithShotResult(Shot shot);
        void DisplayPlayerGrid();
        void DisplayWelcomeMessage();
        Coordinates AskAboutShotCoordinates();
        void DisplayAlreadyShotMessage();
    }
}