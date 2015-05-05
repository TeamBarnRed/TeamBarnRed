using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Core
{
    public class Player : System.IComparable<Player>
    {
        private string name;
        private int score;

        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new System.ArgumentOutOfRangeException("Name of player cannot be null or empty!");
                }

                this.name = value;
            }
        }
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                if (value == null)
                {
                    throw new System.ArgumentOutOfRangeException("Name of player cannot be null or empty!");
                }

                this.score = value;
            }
        }

        public int CompareTo(Player other)
        {
            return this.Score.CompareTo(other.Score);
        }
        public override string ToString()
        {
            return this.name + " --> " + this.score;
        }
        
    }
}
