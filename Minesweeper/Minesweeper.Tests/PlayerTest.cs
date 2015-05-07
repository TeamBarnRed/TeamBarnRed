﻿namespace Minesweeper.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Core;

    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePlayerWithEmptyNameTest()
        {
            Player player = new Player("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetPlayerEmptyNameTest()
        {
            Player player = new Player("Pesho");
            player.Name = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetPlayerNegativeScoreTest()
        {
            Player player = new Player("Pesho");
            player.Score = -1;
        }

        [TestMethod]
        public void ComparePlayerToAnotherPlayerWithLowerScoreTest()
        {
            Player player = new Player("Pesho");
            player.Score = 17;
            Player player2 = new Player("Pesho");
            player2.Score = 15;

            int resultOutput = player.CompareTo(player2);

            Assert.AreEqual(resultOutput, 1);
        }

        [TestMethod]
        public void ComparePlayerToAnotherPlayerWithSameScoreTest()
        {
            Player player = new Player("Pesho");
            player.Score = 15;
            Player player2 = new Player("Pesho");
            player2.Score = 15;

            int resultOutput = player.CompareTo(player2);

            Assert.AreEqual(resultOutput, 0);
        }

        [TestMethod]
        public void ComparePlayerToAnotherPlayerWithHigherScoreTest()
        {
            Player player = new Player("Pesho");
            player.Score = 15;
            Player player2 = new Player("Pesho");
            player2.Score = 17;

            int resultOutput = player.CompareTo(player2);

            Assert.AreEqual(resultOutput, -1);
        }

        [TestMethod]
        public void PlayerToStringTest()
        {
            Player player = new Player("Pesho");
            player.Score = 15;

            string resultOutput = player.ToString();
            string expectedOutput = string.Format("{0} --> {1}", player.Name, player.Score);

            Assert.AreEqual(resultOutput, expectedOutput);
        }
    }
}
