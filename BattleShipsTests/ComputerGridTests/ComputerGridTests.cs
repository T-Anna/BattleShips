using BattleShips;
using Moq;
using Xunit;

namespace BattleShipsTests.ComputerGridTests
{
    public class ComputerGridTests
    {
        [Fact]
        public void ShouldReturnFieldForGivenCoordinates()
        {
            var fields = new IField[10, 10];
            var fieldMock = new Mock<IField>();
            fields[1, 1] = fieldMock.Object;

            var grid = new ComputerGrid(fields);
            var fieldResultFromTheGrid = grid.GetField(new Coordinates(1, 1));

            Assert.Equal(fieldMock.Object, fieldResultFromTheGrid);
        }
    }
}
