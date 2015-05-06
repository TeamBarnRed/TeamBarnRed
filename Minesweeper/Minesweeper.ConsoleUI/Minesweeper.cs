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
            string currentBoard = PrintBoard(Game.Board);

            Game.Board.OnSuccessfullyOpenedField += (field) =>
            {
                RedrawGameUI(Game.Board);
            };

            Game.Board.OnSteppedOnMine += (field) =>
            {
                RedrawGameUI(Game.Board);
                Console.WriteLine("Game over! You stepped on a mine.");
            };

            Game.Board.OnBoardSolved += () =>
            {
                RedrawGameUI(Game.Board);
                Console.WriteLine("You won! Congratulations!");
            };

            bool readCommand = true;
            RedrawGameUI(Game.Board);

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
                            Game.Run();
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

        private static void RedrawGameUI(Board board)
        {
            Console.Clear();

            Console.WriteLine("Welcome to the game “Minesweeper”. " +
                "Try to reveal all cells without mines. " +
                "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                "and 'exit' to quit the game.");
            Console.WriteLine(PrintBoard(board));
            Console.WriteLine("Enter command:");
        }

        private static void ExitGame()
        {
            Console.WriteLine("Good bye!");
        }

        private static string PrintBoard(Board board)
        {
            var sb = new System.Text.StringBuilder();

            // Append header
            sb.Append("   ");
            for (int i = 0; i < board.Columns; i++)
            {
                sb.AppendFormat(" {0}", i);
            }
            sb.AppendLine(" ");

            // Append rows
            sb.Append("   ");
            sb.Append('-', board.Columns * 2 + 1);
            sb.AppendLine(" ");
            for (int i = 0; i < board.Rows; i++)
            {
                sb.AppendFormat("{0} |", i);
                for (int j = 0; j < board.Columns; j++)
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
            sb.Append('-', board.Columns * 2 + 1);
            sb.AppendLine(" ");

            return sb.ToString();
        }
	}
}
