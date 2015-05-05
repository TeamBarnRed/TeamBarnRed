namespace Minesweeper.Core
{
    using System;

    internal class Minesweeper
    {
        public static void Main()
        {
            GameEngine minesweeperEngine = new GameEngine();
            minesweeperEngine.Run();
        }
    }
}
