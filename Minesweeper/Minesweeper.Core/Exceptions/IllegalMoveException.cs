namespace Minesweeper.Core.Exceptions
{
    using System;

    public class IllegalMoveException : ApplicationException
    {
        public IllegalMoveException()
        { }

        public IllegalMoveException(IField field)
        {
            this.Field = field;
        }

        public IllegalMoveException(IField field, string message)
            : base(message)
        {
            this.Field = field;
        }

        public IField Field { get; private set; }
    }
}
