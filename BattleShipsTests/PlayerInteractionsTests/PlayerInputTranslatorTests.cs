using BattleShips;
using Xunit;

namespace BattleShipsTests.PlayerInteractionsTests
{
    public class PlayerInputTranslatorTests
    {
        [Theory]
        [InlineData("A5", 5, 0)]
        [InlineData("C0", 0, 2)]
        [InlineData("J9", 9, 9)]
        public void ShouldTranslateUserInputToCoordinates(string input, int expectedRow, int expectedColumn)
        {
            var translator = new PlayerInputTranslator();
            var coordinates = translator.TranslateCoordinates(input);
            Assert.Equal(expectedRow, coordinates.Row);
            Assert.Equal(expectedColumn, coordinates.Column);
        }
    }
}
