namespace Minesweeper.ConsoleUI
{
    using System;

    using Core;
    using Core.Constants;

	internal class Minesweeper
	{
		public static void Main ()
		{
            int rows = GameConstants.FieldRows;
            int cols = GameConstants.FieldCols;
            int minesCount = GameConstants.FieldMinesCount;

            GameEngine minesweeperEngine = GameEngine.Instance(rows, cols, minesCount);
            minesweeperEngine.Run();
		}
	}
}
