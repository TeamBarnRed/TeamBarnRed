namespace Minesweeper.Core
{
    public interface IField
    {
        int Column { get; set; }
        int Row { get; set; }
        FieldType Type { get; set; }
        int Value { get; set; }
    }
}