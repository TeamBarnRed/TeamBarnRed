namespace Minesweeper.Core
{
    using System;

    /// <summary>
    /// Class hoding the game player
    /// </summary>
    public class Player : IComparable<IPlayer>, IPlayer
    {
        private string name = "";
        private int score = 0;

        /// <summary>
        /// Player constructor method.
        /// </summary>
        /// <param name="name">The player name.</param>
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
                    throw new ArgumentNullException("Name of player cannot be null or empty!");
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Player score cannot be negative number!");
                }

                this.score = value;
            }
        }

        /// <summary>
        /// Compare player to other player by score.
        /// </summary>
        /// <param name="player">Other player object.</param>
        public int CompareTo(IPlayer other)
        {
            return this.Score.CompareTo(other.Score);
        }

        /// <summary>
        /// Print player
        /// </summary>
        public override string ToString()
        {
            return this.Name + " --> " + this.Score;
        }
    }
}
