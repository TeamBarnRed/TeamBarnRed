﻿namespace Minesweeper.Core
{
    using System;

    public class Player : IComparable<Player>
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

        public int CompareTo(Player other)
        {
            return this.Score.CompareTo(other.Score);
        }

        public override string ToString()
        {
            return this.Name + " --> " + this.Score;
        }
        
    }
}
