namespace Minesweeper.Core
{
    internal class Field : IField
    {
        public Field(int value, int row, int col)
        {
            this.Value = value;
            this.Row = row;
            this.Column = col;
            this.Type = FieldType.Closed;
        }

        public int Value { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public FieldType Type { get; set; }
    }
}