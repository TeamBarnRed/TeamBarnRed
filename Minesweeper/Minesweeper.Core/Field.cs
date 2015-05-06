namespace Minesweeper.Core
{
    using System;

    public class Field
    {
        private int value = 0;
        private FieldStatus status = FieldStatus.Closed;

        public Field()
        {
            this.Value = 0;
            this.Status = FieldStatus.Closed;
        }

        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Field value cannot be negative number!");
                }

                this.value = value;
            }
        }

        public FieldStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }
    }
}