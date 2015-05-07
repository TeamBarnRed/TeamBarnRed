namespace Minesweeper.ConsoleUI
{
    using System;
    using Core;

    /// <summary>
    /// Class hoding the console UI game
    /// </summary>
    internal class Minesweeper
    {
        private const string ExitCommand = "exit";
        private const int RowsCount = 10;
        private const int ColsCount = 10;
        private const int MinesCount = 10;
        private static Exception gameException = null;
        private static bool isGameOver = false;

        /// <summary>
        /// Console UI game main method.
        /// </summary>
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
                        isGameOver = false;
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

                PrintBoard(isGameOver);
                Console.Write(Environment.NewLine + "Enter command: ");
                command = Console.ReadLine();
            }

            Console.WriteLine("Good bye!");
            Console.ReadKey();
        }

        private static void PrintScoreBoard()
        {
            foreach (var player in Game.TopPlayers)
            {
                Console.WriteLine(player);
            }
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
                gameException = new FormatException("Invalid coordinates! Enter numbers separated with space!");
            }
            catch (InvalidOperationException)
            {
                gameException = new InvalidOperationException("Game is over! Please type \"restart\" command to start a new game!");
            }
            catch (IndexOutOfRangeException)
            {
                gameException = new IndexOutOfRangeException("Invalid coordinates! Enter numbers separated with space!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                gameException = exception;
            }
        }

        private static void OnGameOver(GameOverEventArgs args)
        {
            if (args.IsWon)
            {
                gameException = new ApplicationException("Congratulations! You successfully solved the game!");
            }
            else
            {
                gameException = new ApplicationException("Game over! You stepped on mine!");
            }

            Console.WriteLine("Your name: ");
            string name = Console.ReadLine();
            var player = new Player(name, args.Score);
            Game.AddPlayerToScoreBoard(player);

            isGameOver = true;
        }

        private static void PrintBoard(bool revealBoard = false)
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
                        fieldContent = " ";

                        if (revealBoard)
                        {
                            fieldContent = Game.Board[i, j].Value.ToString();
                        }
                    }
                    else
                    {
                        fieldContent = Game.Board[i, j].Value.ToString();

                        if (revealBoard)
                        {
                            fieldContent = "*";
                        }
                    }

                    Console.Write(" {0}", fieldContent);
                }
                Console.WriteLine(" |");
            }

            // Print footer
            Console.Write("   ");
            Console.Write(new string('-', Game.Board.Columns * 2 + 1));
            Console.WriteLine(" ");

            // Print game exception if has
            if (gameException != null) {
                Console.WriteLine(gameException.Message);
                gameException = null;
            }
        }
    }
}
