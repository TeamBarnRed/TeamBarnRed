namespace Minesweeper.Core
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Exceptions;

    public class GameBoard : IEnumerable<IField>
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

        public GameBoard(int rows, int columns, int minesCount)
        {
            if (rows < 0 || rows > MaxRows)
            {
                throw new ArgumentOutOfRangeException("Board rows must be in range 0..." + MaxRows);
            }

            if (columns < 0 || columns > MaxColumns)
            {
                throw new ArgumentOutOfRangeException("Board columns must be in range 0..." + MaxColumns);
            }

            if (minesCount >= columns * rows || minesCount < 0)
            {
                throw new ArgumentOutOfRangeException("Mines count must be positive number and less than the board size!");
            }

            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;

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
        }

        public int Columns
        {
            get { return this.columns; }
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

        public override string ToString()
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
                    sb.AppendFormat(" {0}", this[i, j].Value);
                }
                sb.AppendLine(" |");
            }

            //generates -----------------
            sb.Append("   ");
            sb.Append('-', columns * 2 + 1);
            sb.AppendLine(" ");

            return sb.ToString();
        }

        /// <summary>
        /// Opens the selected field
        /// </summary>
        /// <param name="field">The field to be opened</param>
        public void OpenField(IField field)
        {
            if (!canMove)
            {
                throw new InvalidOperationException("This board is already solved or player stepped on mine! Please reinit first!");
            }

            if (field != this[field.Row, field.Column])
            {
                throw new InvalidFieldException(field, "This field does not exists in the board!");
            }

            switch (field.Type)
            {
                case FieldType.Closed:
                    field.Type = FieldType.Closed;
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

                this.fields[row, column].Type = FieldType.Mine;
                insertedMinesCount++;
            }
            while (insertedMinesCount < this.minesCount);
        }

        private void SetFieldValue(IField field)
        {
            int value = 0;

            for (int row = field.Row - 1; row <= field.Row + 1; row++)
            {
                if (row < 0 || row >= this.rows)
                {
                    continue;
                }

                for (int col = field.Column - 1; col <= field.Column + 1; col++)
                {
                    if (col < 0 || col >= this.columns)
                    {
                        continue;
                    }

                    if (this[row, col].Type == FieldType.Mine)
                    {
                        value++;
                    }
                }
            }

            field.Value = value;
        }

        private void InitializeEmptyFields()
        {
            var emptyFields = from field in this
                              where field.Type == FieldType.Closed
                              select field;

            foreach (var field in emptyFields)
            {
                SetFieldValue(field);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}