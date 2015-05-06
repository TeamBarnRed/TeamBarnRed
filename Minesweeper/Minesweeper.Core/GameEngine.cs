namespace Minesweeper.Core
{
    using System;
    using Interfaces;

    public class GameEngine : IEngine
    {
        private static GameEngine gameEngineInstance = null;

        public GameBoard Board { get; private set; }

        private GameEngine(int rows, int cols, int minesCount)
        {
            this.Board = new GameBoard(rows, cols, minesCount);
        }

        public static GameEngine Instance(int rows, int cols, int minesCount)
        {
            if (gameEngineInstance == null)
            {
                gameEngineInstance = new GameEngine(rows, cols, minesCount);
            }

            return gameEngineInstance;
        }

        public virtual void Run()
        {

        }
    }
}
