namespace Minesweeper.Core
{
    using System;

    /// <summary>
    /// Class hoding the game over data
    /// </summary>
    public class GameOverEventArgs : EventArgs
    {
        /// <summary>
        /// Game over constructor method.
        /// </summary>
        /// <param name="isWon">Is user won when game is over.</param>
        /// <param name="score">User score when game is over.</param>
        internal GameOverEventArgs(bool isWon, int score)
        {
            this.IsWon = isWon;
            this.Score = score;
        }

        public bool IsWon { get; private set; }

        public int Score { get; private set; }
    }
}
