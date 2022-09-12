namespace BattleShips
{
    public class ShotCoordinatesTaker : IShotCoordinatesTaker
    {
        private readonly IPlayerInteractions userInteractions;
        private readonly IPlayerGrid playerGrid;

        public ShotCoordinatesTaker(IPlayerInteractions userInteractions, IPlayerGrid playerGrid)
        {
            this.userInteractions = userInteractions;
            this.playerGrid = playerGrid;
        }

        public Coordinates DetermineShotCoordinates()
        {
            var shotCoordinates = userInteractions.AskAboutShotCoordinates();

            while (playerGrid.WasShotBefore(shotCoordinates))
            {
                shotCoordinates = userInteractions.AskAboutShotCoordinates();
            }
            return shotCoordinates;
        }
    }
}
