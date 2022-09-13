using System;

namespace BattleShips
{
    public class PlayerInteractions : IPlayerInteractions
    {
        private readonly IPlayerInputTranslator translator;
        private readonly IPlayerGrid playerGrid;

        public PlayerInteractions(IPlayerInputTranslator translator, IPlayerGrid playerGrid)
        {
            this.translator = translator;
            this.playerGrid = playerGrid;
        }

        public void DisplayWelcomeMessage()
        {
            Console.Write("Welcome to BattleShips!\nTo win the game you will need to sink 3 ships placed on the grid:\n" +
                "- 1x Battleship (5 fields long)\n" +
                "- 2x Destroyer (4 fields long)\n" +
                "Ship is sunk when all fields it is placed on are hit. \n" +
                "To shoot given field please enter coordinate of your shot as 2 characters - for example 'A5' (capital letter as a column and number as a row) and press enter.\n" +
                "Please note that program is not proof against incorrect format of the input \n\n");
        }

        public void DisplayPlayerGrid()
        {
            Console.WriteLine("  A B C D E F G H I J");

            for (int r = 0; r < 10; r++)
            {
                Console.Write($"{r} ");
                for (int c = 0; c < 10; c++)
                {
                    var state = playerGrid.GetShotResultForGivenCoordinates(new Coordinates(r, c));
                    if (state == ShotResultEnum.ShotResult.Missed)
                    {
                        Console.Write("- ");
                    }
                    else if (state == ShotResultEnum.ShotResult.Hit)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write("? ");
                    }
                }
                Console.WriteLine();
            }
        }

        public Coordinates AskAboutShotCoordinates()
        {
            Console.WriteLine("Enter coordinates of your shot: ");
            string coordinatesString = Console.ReadLine();
            var coordinates = translator.TranslateCoordinates(coordinatesString);
            return coordinates;
        }

        public void DisplayAlreadyShotMessage()
        {
            Console.WriteLine("You already shot that field.");
        }

        public void DisplayMessageWithShotResult(Shot shot)
        {
            if (shot.ShotResult == ShotResultEnum.ShotResult.Missed)
            {
                DisplayMissedMessage();
            }
            else if (shot.ShotResult == ShotResultEnum.ShotResult.Hit)
            {
                if (shot.ShipHit.IsSunk())
                {
                    DisplaySunkMessage(shot.ShipHit);
                }
                else
                {
                    DisplayHitMessage(shot.ShipHit);
                }
            }
        }

        public void DisplayGameEndMessage()
        {
            Console.WriteLine("You sunk all ships. You won the game! Congratulations!");
        }

        private void DisplayMissedMessage()
        {
            Console.WriteLine("Shot missed. Try again.");
        }

        private void DisplayHitMessage(IShip Ship)
        {
            Console.WriteLine($"You hit {Ship.ShipType}!");
        }

        private void DisplaySunkMessage(IShip Ship)
        {
            Console.WriteLine($"You sunk {Ship.ShipType}!");
        }
    }
}
