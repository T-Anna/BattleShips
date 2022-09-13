using BattleShips;
using Moq;
using System;
using System.IO;
using Xunit;
using static BattleShips.ShipTypeEnum;
using static BattleShips.ShotResultEnum;

namespace BattleShipsTests.PlayerInteractionsTests
{
    public class PlayerInteractionsTests
    {
        private readonly Mock<IPlayerInputTranslator> mockTranslator;
        private readonly Mock<IPlayerGrid> mockPlayerGrid;
        private readonly PlayerInteractions playerInteractions;

        public PlayerInteractionsTests()
        {
            mockTranslator = new Mock<IPlayerInputTranslator>();
            mockPlayerGrid = new Mock<IPlayerGrid>();
            playerInteractions = new PlayerInteractions(mockTranslator.Object, mockPlayerGrid.Object);
        }

        [Fact]
        public void PlayerGridIsDisplayedInCorrectFormat()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            mockPlayerGrid.Setup(x => x.GetShotResultForGivenCoordinates(It.Is<Coordinates>(x => x.Row == 0 & x.Column == 0))).Returns(ShotResult.Missed);
            mockPlayerGrid.Setup(x => x.GetShotResultForGivenCoordinates(It.Is<Coordinates>(x =>x.Row==0 & x.Column == 1))).Returns(ShotResult.Hit);

            playerInteractions.DisplayPlayerGrid();
            Assert.Equal("  A B C D E F G H I J\r\n" +
                "0 - X ? ? ? ? ? ? ? ? \r\n" +
                "1 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "2 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "3 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "4 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "5 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "6 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "7 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "8 ? ? ? ? ? ? ? ? ? ? \r\n" +
                "9 ? ? ? ? ? ? ? ? ? ? \r\n" 
                , 
                stringWriter.ToString());
        }

        [Fact]
        public void AskAboutShotCoordinatesReturnsInputTranslatedToCoordinates()
        {
            var stringReader = new StringReader("A5");
            Console.SetIn(stringReader);
            var expectedCoordinates = new Coordinates(0, 5);
            mockTranslator.Setup(x => x.TranslateCoordinates("A5")).Returns(expectedCoordinates);

            var resultCoordinates = playerInteractions.AskAboutShotCoordinates();

            mockTranslator.Verify(x => x.TranslateCoordinates("A5"));
            Assert.Equal(expectedCoordinates, resultCoordinates);
        }

        [Fact]
        public void DisplayMessageWithShotResultDisplaysCorrectMessage_MissedShot()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var shot = new Shot(new Coordinates(3, 3));
            shot.ShotResult = ShotResult.Missed;

            playerInteractions.DisplayMessageWithShotResult(shot);

            Assert.Equal("Shot missed. Try again.\r\n", stringWriter.ToString());
        }

        [Fact]
        public void DisplayMessageWithShotResultDisplaysCorrectMessage_HitShot_ShipNotSunk()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var mockShip = new Mock<IShip>();
            mockShip.Setup(x => x.ShipType).Returns(ShipType.Destroyer);
            mockShip.Setup(x => x.IsSunk()).Returns(false);

            var shot = new Shot(new Coordinates(3, 3));
            shot.ShotResult = ShotResult.Hit;
            shot.ShipHit = mockShip.Object;

            playerInteractions.DisplayMessageWithShotResult(shot);

            Assert.Equal("You hit Destroyer!\r\n", stringWriter.ToString());
        }

        [Fact]
        public void DisplayMessageWithShotResultDisplaysCorrectMessage_HitShot_ShipSunk()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var mockShip = new Mock<IShip>();
            mockShip.Setup(x => x.ShipType).Returns(ShipType.Destroyer);
            mockShip.Setup(x => x.IsSunk()).Returns(true);

            var shot = new Shot(new Coordinates(3, 3));
            shot.ShotResult = ShotResult.Hit;
            shot.ShipHit = mockShip.Object;

            playerInteractions.DisplayMessageWithShotResult(shot);

            Assert.Equal("You sunk Destroyer!\r\n", stringWriter.ToString());
        }
    }
}
