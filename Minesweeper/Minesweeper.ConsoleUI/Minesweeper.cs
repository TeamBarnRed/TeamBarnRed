namespace Minesweeper.ConsoleUI
{
    using System;
    using Core;

    internal class Minesweeper
    {
        private const string ExitCommand = "exit";
        private const int RowsCount = 10;
        private const int ColsCount = 10;
        private const int MinesCount = 10;

        public static void Main()
        {
            Game.OnGameOver += OnGameOver;
            Game.Start(RowsCount, ColsCount, MinesCount);

            string command = string.Empty;

            while (command != ExitCommand)
            {
                switch (command)
                {
                    case "restart":
                        Game.Start(RowsCount, ColsCount, MinesCount);
                        break;
                    case "top":
                        PrintScoreBoard();
                        break;
                    default:
                        if (!string.IsNullOrEmpty(command))
                        {
                            ProcessCoordinates(command);
                        }
                        break;
                }

                PrintBoard();
                Console.Write(Environment.NewLine + "Enter command: ");
                command = Console.ReadLine();
            }

            Console.WriteLine("Good bye!");
            Console.ReadKey();
        }

        private static void PrintScoreBoard()
        {
            throw new NotImplementedException();
        }

        private static void ProcessCoordinates(string coordinates)
        {
            string[] coordinatesAsArray = coordinates.Split(' ');
            try
            {
                int row = int.Parse(coordinatesAsArray[0]);
                int col = int.Parse(coordinatesAsArray[1]);
                Game.Board.OpenField(row, col);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid coordinates! Enter numbers separated with space!");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Game is over! Please type \"restart\" command to start a new game!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void OnGameOver(GameOverEventArgs args)
        {
            if (args.IsWon)
            {
                Console.WriteLine("Congratulations! You successfully solved the game!");

            }
            else
            {
                Console.WriteLine("Game over! You stepped on mine!");
            }
            Console.WriteLine("Type \"restart\" to restart the game!");
        }

        private static void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the game “Minesweeper”. " +
                "Try to reveal all cells without mines. " +
                "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                "and 'exit' to quit the game.");

            // Print header
            Console.Write("   ");
            for (int i = 0; i < Game.Board.Columns; i++)
            {
                Console.Write(" {0}", i);
            }
            Console.WriteLine(" ");

            // Print board
            Console.Write("   ");
            Console.Write(new string('-', Game.Board.Columns * 2 + 1));
            Console.WriteLine(" ");
            for (int i = 0; i < Game.Board.Rows; i++)
            {
                Console.Write("{0} |", i);
                for (int j = 0; j < Game.Board.Columns; j++)
                {
                    var fieldContent = "";

                    if (Game.Board[i, j].Type != FieldType.Opened)
                    {
                        fieldContent = "?";
                    }
                    else
                    {
                        fieldContent = Game.Board[i, j].Value.ToString();
                    }

                    Console.Write(" {0}", fieldContent);
                }
                Console.WriteLine(" |");
            }

            // Print footer
            Console.Write("   ");
            Console.Write(new string('-', Game.Board.Columns * 2 + 1));
            Console.WriteLine(" ");
        }
    }
}
