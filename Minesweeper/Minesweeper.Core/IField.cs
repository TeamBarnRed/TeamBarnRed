namespace Minesweeper.Core
{
    public interface IField
    {
        int Column { get; }
        int Row { get; }
        FieldType Type { get; }
        int Value { get; }
    }
}