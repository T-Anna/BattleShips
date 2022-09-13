namespace BattleShips
{
    public class ShotCoordinatesTaker : IShotCoordinatesTaker
    {
        private readonly IPlayerInteractions playerInteractions;
        private readonly IPlayerGrid playerGrid;

        public ShotCoordinatesTaker(IPlayerInteractions playerInteractions, IPlayerGrid playerGrid)
        {
            this.playerInteractions = playerInteractions;
            this.playerGrid = playerGrid;
        }

        public Coordinates DetermineShotCoordinates()
        {
            var shotCoordinates = playerInteractions.AskAboutShotCoordinates();

            while (playerGrid.WasShotBefore(shotCoordinates))
            {
                playerInteractions.DisplayAlreadyShotMessage();
                shotCoordinates = playerInteractions.AskAboutShotCoordinates();
            }
            return shotCoordinates;
        }
    }
}
