namespace Minesweeper.ConsoleUI
{
    using System;
    using Core;
    
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
            var board = new GameBoard(5, 10, 49);
            Console.WriteLine(board.ToString());
            Console.ReadLine();
		}
	}
}
