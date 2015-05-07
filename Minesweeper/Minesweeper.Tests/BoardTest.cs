namespace Minesweeper.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Core;
    using Core.Exceptions;

    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithNegativeRowsTest()
        {
            int rows = -1;
            int columns = 5;
            int minesCount = 5;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithExceededRowsTest()
        {
            int rows = Board.MaxRows + 1;
            int columns = 5;
            int minesCount = 5;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithNegativeColumnsTest()
        {
            int rows = 5;
            int columns = -1;
            int minesCount = 5;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithExceededColumnsTest()
        {
            int rows = 5;
            int columns = Board.MaxColumns + 1;
            int minesCount = 5;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithNegativeMinesCountTest()
        {
            int rows = 5;
            int columns = 5;
            int minesCount = -5;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateBoardWithExceededMinesCountTest()
        {
            int rows = 5;
            int columns = 5;
            int minesCount = Board.MaxRows * Board.MaxColumns;

            Board board = new Board(rows, columns, minesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void OpenFieldWithNonExistingFieldTest()
        {
            Board board = new Board(5, 5, 5);
            board.OpenField(6, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalMoveException))]
        public void OpenSameFieldTwoTimesTest()
        {
            Board board = new Board(5, 5, 0);
            board.OpenField(1, 1);
            board.OpenField(1, 1);
        }
    }
}
