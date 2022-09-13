using BattleShips;
using Xunit;
using static BattleShips.ShipTypeEnum;

namespace BattleShipsTests.ShipTests
{
    public class ShipTests
    {
        [Theory]
        [InlineData(ShipType.BattleShip, 5)]
        [InlineData(ShipType.Destroyer, 4)]

        public void ShipIsSunk_WhenItIsHitAsManyTimesAsItsSize(ShipType shipType, int numberOfHits)
        {
            var ship = new Ship(shipType);
            for (int i = 0; i < numberOfHits; i++)
            {
                ship.TakeHit();
            }
            Assert.True(ship.IsSunk());
        }

        [Theory]
        [InlineData(ShipType.BattleShip, 2)]
        [InlineData(ShipType.Destroyer, 3)]

        public void ShipIsNotSunk_WhenShipIsHitLessTimesThanShipsSize(ShipType shipType, int numberOfHits)
        {
            var ship = new Ship(shipType);
            for (int i = 0; i < numberOfHits; i++)
            {
                ship.TakeHit();
            }
            Assert.False(ship.IsSunk());
        }
    }
}
