namespace Minesweeper.Core
{
    using System;

    class Board
    {
        private Field[,] board = null;
        private int minesCount = 0;

        public Board(int rows, int cols, int minesCount)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.MinesCount = minesCount;
            this.Board = new Field[rows, cols];
        }

        private int Rows { get; set; }

        private int Cols { get; set; }

        internal int MinesCount
        {
            get
            {
                return this.minesCount;
            }

            set
            {
                if (value <= 0 || this.Rows * this.Cols <= value)
                {
                    throw new ArgumentOutOfRangeException("The number of mines should be at least 1.");
                }

                this.minesCount = value;
            }
        }

        private Field[,] Board
        {
            get
            {
                return this.board;
            }

            set
            {
                this.board = value;
            }
        }
    }
}
