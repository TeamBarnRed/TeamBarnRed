namespace Minesweeper.Core
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Exceptions;

    /// <summary>
    /// Class containing the game board
    /// </summary>
    public class Board : IEnumerable<IField>
    {
        public const int MaxRows = 100;
        public const int MaxColumns = 100;

        private int rows = 0;
        private int columns = 0;
        private int minesCount = 0;
        private int openedFieldsCount = 0;
        private bool canMove = true;
        private IField[,] fields = null;
        private Random random = new Random();

        /// <summary>
        /// Board constructor method.
        /// </summary>
        /// <param name="rows">The amount of rows on the game board.</param>
        /// <param name="columns">The amount of columns on the game board.</param>
        /// <param name="minesCount">The amount of mines on the game board.</param>
        public Board(int rows, int columns, int minesCount)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.MinesCount = minesCount;
            this.fields = new Field[rows, columns];
            this.InitializeFields();
        }

        public delegate void SteppedOnMine(IField field);

        public delegate void BoardSolved();

        /// <summary>
        /// The event is fired when the player steps on mine
        /// </summary>
        public event SteppedOnMine OnSteppedOnMine;

        /// <summary>
        /// The event is fired when the player has solved the board
        /// </summary>
        public event BoardSolved OnBoardSolved;

        public int Rows
        {
            get { return this.rows; }

            private set
            {
                if (value < 0 || value > MaxRows)
                {
                    throw new ArgumentOutOfRangeException("Board rows must be in range 0..." + MaxRows);
                }
                this.rows = value;
            }
        }

        public int Columns
        {
            get { return this.columns; }

            private set
            {
                if (value < 0 || value > MaxColumns)
                {
                    throw new ArgumentOutOfRangeException("Board columns must be in range 0..." + MaxColumns);
                }
                this.columns = value;
            }
        }

        public int MinesCount
        {
            get { return this.minesCount; }

            private set
            {
                if (value >= columns * rows || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Mines count must be positive number and less than the board size!");
                }
                this.minesCount = value;
            }
        }

        public int OpenedFieldsCount
        {
            get { return this.openedFieldsCount; }
        }

        public IField this[int row, int col]
        {
            get { return this.fields[row, col]; }

            private set
            {
                this.fields[row, col] = value;
            }
        }

        public IEnumerator<IField> GetEnumerator()
        {
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.columns; col++)
                {
                    yield return this[row, col];
                }
            }
        }

        /// <summary>
        /// Opens the selected field
        /// </summary>
        /// <param name="field">The field to be opened</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InvalidFieldException"></exception>
        /// <exception cref="IllegalMoveException"></exception>
        public void OpenField(int row, int col)
        {
            var field = (Field)this[row, col];

            if (!canMove)
            {
                throw new InvalidOperationException("This board is already solved or player stepped on mine! Please restart first!");
            }

            if (field != this[field.Row, field.Column])
            {
                throw new InvalidFieldException(field, "This field does not exist in the board!");
            }

            switch (field.Type)
            {
                case FieldType.Closed:
                    field.Type = FieldType.Opened;
                    this.openedFieldsCount++;

                    if (this.openedFieldsCount + this.minesCount == this.rows * this.columns)
                    {
                        OnBoardSolved();
                        this.canMove = false;
                    }

                    break;
                case FieldType.Opened:
                    throw new IllegalMoveException(field, "The selected field is already opened!");
                case FieldType.Mine:
                    OnSteppedOnMine(field);
                    this.canMove = false;

                    break;
            }
        }

        private void InitializeFields()
        {
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.columns; col++)
                {
                    this[row, col] = new Field(0, row, col);
                }
            }

            this.AddMines();
            this.InitializeEmptyFields();
        }

        private void AddMines()
        {
            int insertedMinesCount = 0;

            do
            {
                int row = this.random.Next(0, this.rows);
                int column = this.random.Next(0, this.columns);

                if (this.fields[row, column].Type == FieldType.Mine)
                {
                    continue;
                }

                ((Field)this.fields[row, column]).Type = FieldType.Mine;
                insertedMinesCount++;
            }
            while (insertedMinesCount < this.minesCount);
        }

        private void SetFieldValue(Field field)
        {
            field.Value = 0;

            for (int row = field.Row - 1; row <= field.Row + 1; row++)
            {
                if (row < 0 || row >= this.rows)
                {
                    continue;
                }

                for (int col = field.Column - 1; col <= field.Column + 1; col++)
                {
                    if (col > 0 && col < this.columns &&
                        this[row, col].Type == FieldType.Mine)
                    {
                        field.Value++;
                    }
                }
            }
        }

        private void InitializeEmptyFields()
        {
            var emptyFields = from field in this
                              where field.Type == FieldType.Closed
                              select field;

            foreach (var field in emptyFields)
            {
                SetFieldValue((Field)field);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}