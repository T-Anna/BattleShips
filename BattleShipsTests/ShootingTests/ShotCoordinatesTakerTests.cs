using BattleShips;
using Moq;
using Xunit;

namespace BattleShipsTests.ShootingTests
{
    public class ShotCoordinatesTakerTests
    {
        private readonly Mock<IPlayerInteractions> userInteractionsMock;
        private readonly Mock<IPlayerGrid> playerGridMock;
        private readonly ShotCoordinatesTaker shotCoordinatesTaker;

        public ShotCoordinatesTakerTests()
        {
            userInteractionsMock = new Mock<IPlayerInteractions>();
            playerGridMock = new Mock<IPlayerGrid>();
            shotCoordinatesTaker = new ShotCoordinatesTaker(userInteractionsMock.Object, playerGridMock.Object);
        }

        [Fact]
        public void ShouldAskPlayerAboutShotCoordinates()
        {
            shotCoordinatesTaker.DetermineShotCoordinates();
            userInteractionsMock.Verify(x => x.AskAboutShotCoordinates(), Times.Once);
        }

        [Fact]
        public void ShouldRepeatAskingAboutShotCoordinates_WhenCoordinatesWereShotBefore()
        {
            var alreadyShotCoordinates = new Coordinates(3, 5);
            var notShotCoordinates = new Coordinates(2, 3);

            userInteractionsMock.SetupSequence(x => x.AskAboutShotCoordinates()).Returns(alreadyShotCoordinates)
                                                                                .Returns(notShotCoordinates);
            playerGridMock.Setup(x => x.WasShotBefore(alreadyShotCoordinates)).Returns(true);
            playerGridMock.Setup(x => x.WasShotBefore(notShotCoordinates)).Returns(false);

            shotCoordinatesTaker.DetermineShotCoordinates();
            userInteractionsMock.Verify(x => x.AskAboutShotCoordinates(), Times.Exactly(2));
            userInteractionsMock.Verify(x => x.DisplayAlreadyShotMessage(), Times.Once);
        }
    }
}
