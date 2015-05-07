﻿namespace Minesweeper.Core
{
    public static class Game
    {
        public const int TopPlayersCount = 5;

        private static Player[] topPlayers = new Player[TopPlayersCount];

        public static Board Board { get; private set; }

        public delegate void GameOverHandler(GameOverEventArgs args);

        public static event GameOverHandler OnGameOver;

        public static void Start(int rows, int columns, int mines)
        {
            Board = new Board(rows, columns, mines);
            Board.OnBoardSolved += () =>
            {
                var eventArgs = new GameOverEventArgs(true, Board.OpenedFieldsCount);
                OnGameOver(eventArgs);
            };
            Board.OnSteppedOnMine += (field) =>
            {
                var eventArgs = new GameOverEventArgs(false, Board.OpenedFieldsCount);
                OnGameOver(eventArgs);
            };
        }

        public static void AddPlayerToScoreBoard(Player player)
        {

        }

        /*
        private static void InitializeTopPlayers()
        {
            topPlayers = new List<Player>();
            topPlayers.Capacity = maxTopPlayers;
        }
        private static bool CheckHighScores(int score)
        {
            if (topPlayers.Capacity > topPlayers.Count)
            {
                return true;
            }

            foreach (Player currentPlayer in topPlayers)
            {
                if (currentPlayer.Score < score)
                {
                    return true;
                }
            }
            return false;
        }

        private static void topadd(ref Player player)
        {
            if (topPlayers.Capacity > topPlayers.Count)
            {
                topPlayers.Add(player);
                topPlayers.Sort();
            }
            else
            {
                topPlayers.RemoveAt(topPlayers.Capacity - 1);
                topPlayers.Add(player);
                topPlayers.Sort();
            }


        }

        private static void top()
        {
            Console.WriteLine("Scoreboard");
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine((int)(i + 1) + ". " + topPlayers[i]);
            }
        }

        private static void Menu()
        {
            InitializeTopPlayers();
            string str = "restart";
            int choosenRow = 0;
            int chosenColumn = 0;

            while (str != "exit")
            {
                if (str == "restart")
                {
                    InitializeGameBoard();
                    Console.WriteLine("Welcome to the game “Minesweeper”. " +
                        "Try to reveal all cells without mines. " +
                        "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                        "and 'exit' to quit the game.");
                    board.PrintGameBoard();
                }
                else if (str == "exit")
                {
                    Console.WriteLine("Good bye!");
                    Console.Read();
                }
                else if (str == "top")
                {
                    top();
                }
                else if (str == "coordinates")
                {
                    try
                    {
                        Board.Status status = board.OpenField(choosenRow, chosenColumn);
                        if (status == Board.Status.SteppedOnAMine)
                        {
                            board.PrintAllFields();
                            int score = board.CountOpenedFields();
                            Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                score +
                                " cells without mines.");

                            if (CheckHighScores(score))
                            {
                                Console.WriteLine("Please enter your name for the top scoreboard: ");
                                string name = Console.ReadLine();
                                Player player = new Player(name, score);
                                topadd(ref player);
                                top();
                            }
                            str = "restart";
                            continue;
                        }

                        else if (status == Board.Status.AlreadyOpened)
                        {
                            Console.WriteLine("Illegal move!");
                        }
                        else if (status == Board.Status.AllFieldsAreOpened)
                        {
                            board.PrintAllFields();
                            int score = board.CountOpenedFields();
                            Console.WriteLine("Congratulations! You won!!");
                            if (CheckHighScores(score))
                            {
                                Console.WriteLine("Please enter your name for the top scoreboard: ");
                                string name = Console.ReadLine();
                                Player player = new Player(name, score);
                                topadd(ref player);
                                // show ranking
                                top();
                            }
                            str = "restart";
                            continue;
                        }
                        else
                        {
                            board.PrintGameBoard();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Illegal move");
                    }
                }

                Console.Write(System.Environment.NewLine + "Enter row and column: ");

                str = Console.ReadLine();
                try
                {
                    choosenRow = int.Parse(str);
                    str = "coordinates";
                }
                catch (Exception)
                {
                    throw new Exception("An error has occured");
                }

                str = Console.ReadLine();
                try
                {
                    chosenColumn = int.Parse(str);
                    str = "coordinates";
                }
                catch (Exception)
                {
                    throw new Exception("An error has occured");
                }
            }
        }*/
    }
}
