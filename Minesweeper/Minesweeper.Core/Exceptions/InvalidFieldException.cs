namespace Minesweeper.Core.Exceptions
{
    public class InvalidFieldException : IllegalMoveException
    {
        public InvalidFieldException()
        { }

        public InvalidFieldException(IField field)
            : base(field)
        { }

        public InvalidFieldException(IField field, string message)
            : base(field, message)
        { }
    }
}
