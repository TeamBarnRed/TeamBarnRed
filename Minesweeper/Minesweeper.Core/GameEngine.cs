namespace Minesweeper.Core
{
    using System;
    using Interfaces;

    public class GameEngine : IEngine
    {
        private static GameEngine gameEngineInstance = null;

        public Board Board { get; private set; }

        private GameEngine(int rows, int cols, int minesCount)
        {
            this.Board = new Board(rows, cols, minesCount);
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
