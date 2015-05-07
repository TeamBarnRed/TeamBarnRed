namespace Minesweeper.Core
{
    public class Field : IField
    {
        internal Field(int value, int row, int col)
        {
            this.Value = value;
            this.Row = row;
            this.Column = col;
            this.Type = FieldType.Closed;
        }

        public int Value { get; internal set; }

        public int Row { get; internal set; }

        public int Column { get; internal set; }

        public FieldType Type { get; internal set; }
    }
}