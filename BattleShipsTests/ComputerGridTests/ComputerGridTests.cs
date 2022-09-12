using BattleShips;
using Moq;
using Xunit;

namespace BattleShipsTests.ComputerGridTests
{
    public class ComputerGridTests
    {
        [Fact]
        public void ShouldReturnFieldForAGivenCoordinates()
        {
            var mockFields = new IField[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    mockFields[i, j] = new Mock<IField>().Object;
                }
            }
            var fieldMock = new Mock<IField>();
            mockFields[1, 1] = fieldMock.Object;

            var grid = new ComputerGrid(mockFields);
            var fieldResultFromTheGrid = grid.GetField(new Coordinates(1, 1));

            Assert.Equal(fieldMock.Object, fieldResultFromTheGrid);
        }
    }
}
