namespace Minesweeper.Core
{
    /// <summary>
    /// Class hoding the game board fields
    /// </summary>
    public class Field : IField
    {
        /// <summary>
        /// Field constructor method.
        /// </summary>
        /// <param name="value">The field character.</param>
        /// <param name="row">The field row in the game board.</param>
        /// <param name="column">The field column in the game board.</param>
        internal Field(int value, int row, int column)
        {
            this.Value = value;
            this.Row = row;
            this.Column = column;
            this.Type = FieldType.Closed;
        }

        public int Value { get; internal set; }

        public int Row { get; internal set; }

        public int Column { get; internal set; }

        public FieldType Type { get; internal set; }
    }
}