namespace Minesweeper.Core
{
    public class Field
    {
        private int value = 0;
        private FieldStatus status = FieldStatus.Closed;

        public Field()
        {
            this.value = 0;
            this.status = FieldStatus.Closed;
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException("Field value cannot be negative number!");
                }
                this.value = value;
            }
        }

        public FieldStatus { get; set; }
    }
}