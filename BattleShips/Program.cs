namespace BattleShips
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = GameInitializer.CreateGame();
            game.Play();
        }
    }
}
