namespace Minesweeper.Core
{
    using System;

    public class GameOverEventArgs : EventArgs
    {
        internal GameOverEventArgs(bool isWon, int score)
        {
            this.IsWon = isWon;
            this.Score = score;
        }

        public bool IsWon { get; private set; }

        public int Score { get; private set; }
    }
}
