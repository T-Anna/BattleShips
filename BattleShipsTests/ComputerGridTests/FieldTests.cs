using BattleShips;
using Moq;
using Xunit;

namespace BattleShipsTests.ComputerGridTests
{
    public class FieldTests
    {
        [Fact]
        public void IsEmptyReturnsFalse_WhenFieldIsOccupiedByShip()
        {
            var field = new Field();
            var mockShip = new Mock<IShip>();
            field.Ship = mockShip.Object;

            Assert.False(field.IsEmpty());
        }

        [Fact]
        public void IsEmptyReturnsTrue_WhenFieldIsNotOccupiedByShip()
        {
            var field = new Field();
            Assert.True(field.IsEmpty());
        }
    }
}
