namespace Minesweeper.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core;
    using Core.Exceptions;
    
    internal class Minesweeper
	{
		public static void Main ()
		{
            //int rows = GameConstants.FieldRows;
            //int cols = GameConstants.FieldCols;
            //int minesCount = GameConstants.FieldMinesCount;
            //
            //GameEngine minesweeperEngine = GameEngine.Instance(rows, cols, minesCount);
            //minesweeperEngine.Run();

            // Test board
            /*var board = new Board(5, 10, 49);
            Console.WriteLine(board.ToString());
            Console.ReadLine();*/


            Game.Run();
            string currentBoard = PrintBoard(Game.GetBoard(), Game.maxRows, Game.maxColumns);

            Game.GetBoard().OnSteppedOnMine += (field) =>
            {
                Console.WriteLine("Game over! You stepped on a mine.");
            };

            Game.GetBoard().OnBoardSolved += () =>
            {
                Console.WriteLine("You won! Congratulations!");
            };

            bool readCommand = true;
            DrawGameUI(Game.GetBoard());

            do
            {
                string currentCommand = Console.ReadLine();

                try
                {
                    switch (currentCommand)
                    {
                        case "exit":
                            readCommand = false;
                            ExitGame();

                            break;
                        case "restart":
                            // todo
                            break;
                        case "top":
                            // todo
                            break;
                        default:
                            string[] commandArguments = currentCommand.Split(' ');
                            int argumentRow = int.Parse(commandArguments[0]);
                            int argumentColumn = int.Parse(commandArguments[1]);

                            Game.OpenField(argumentColumn, argumentRow);

                            ClearGameUI();
                            DrawGameUI(Game.GetBoard());

                            break;
                    }
                }
                catch (FormatException exc)
                {
                    Console.WriteLine("Invalid value for row and/or column.");
                }
                catch (IndexOutOfRangeException exc)
                {
                    Console.WriteLine("Invalid value for row and/or column.");
                }
                catch (InvalidFieldException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (IllegalMoveException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (InvalidOperationException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            while (readCommand);
		}

        private static void DrawGameUI(Board board)
        {
            Console.WriteLine("Welcome to the game “Minesweeper”. " +
                "Try to reveal all cells without mines. " +
                "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                "and 'exit' to quit the game.");
            Console.WriteLine(PrintBoard(board, board.Rows, board.Columns));
            Console.WriteLine("Enter command:");
        }

        private static void ClearGameUI()
        {
            Console.Clear();
        }

        private static void ExitGame()
        {
            Console.WriteLine("Good bye!");
        }

        private static string PrintBoard(Board board, int rows, int columns)
        {
            var sb = new System.Text.StringBuilder();

            // Append header
            sb.Append("   ");
            for (int i = 0; i < columns; i++)
            {
                sb.AppendFormat(" {0}", i);
            }
            sb.AppendLine(" ");

            // Append rows
            sb.Append("   ");
            sb.Append('-', columns * 2 + 1);
            sb.AppendLine(" ");
            for (int i = 0; i < rows; i++)
            {
                sb.AppendFormat("{0} |", i);
                for (int j = 0; j < columns; j++)
                {
                    var fieldContent = "";

                    if (board[i, j].Type == FieldType.Closed || board[i, j].Type == FieldType.Mine)
                    {
                        fieldContent = " ";
                    }
                    else
                    {
                        fieldContent = board[i, j].Value.ToString();
                    }

                    sb.AppendFormat(" {0}", fieldContent);
                }
                sb.AppendLine(" |");
            }

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', columns * 2 + 1);
            sb.AppendLine(" ");

            return sb.ToString();
        }
	}
}
