using System.Collections.Generic;

namespace Minesweeper.Core
{
    /// <summary>
    /// Static class hoding the game
    /// </summary>
    public static class Game
    {
        public const int TopPlayersCount = 5;

        private static List<IPlayer> topPlayers = new List<IPlayer>();

        public static Board Board { get; private set; }

        public static IPlayer[] TopPlayers
        {
            get { return topPlayers.ToArray(); }
        }

        public delegate void GameOverHandler(GameOverEventArgs args);

        public static event GameOverHandler OnGameOver;

        /// <summary>
        /// Game start method.
        /// </summary>
        /// <param name="rows">The game board rows.</param>
        /// <param name="columns">The game board columns.</param>
        /// <param name="mines">The game board mines.</param>
        public static void Start(int rows, int columns, int mines)
        {
            Board = new Board(rows, columns, mines);
            Board.OnBoardSolved += () =>
            {
                var eventArgs = new GameOverEventArgs(true, Board.OpenedFieldsCount);
                OnGameOver(eventArgs);
            };
            Board.OnSteppedOnMine += (field) =>
            {
                var eventArgs = new GameOverEventArgs(false, Board.OpenedFieldsCount);
                OnGameOver(eventArgs);
            };
        }

        public static void AddPlayerToScoreBoard(IPlayer player)
        {
            topPlayers.Add(player);
            topPlayers.Sort();

            if (topPlayers.Count > TopPlayersCount)
            {
                topPlayers.RemoveAt(topPlayers.Count - 1);
            }
        }
    }
}
