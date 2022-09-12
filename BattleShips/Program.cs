
using BattleShips.PlacingComputerShips;
using System;
using System.Collections.Generic;
using static BattleShips.ShipTypeEnum;

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
