using System.Collections.Generic;
using System;
using UnityEngine;

namespace Algorithm
{
    public class Board
    {
        public Board()
        {
            cellState = new State[ROWS, COLS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    cellState[i, j] = State.Empty;
                }
            }
        }

        public Board(Board board) : this()
        {
            Array.Copy(board.cellState, cellState, board.cellState.Length);
        }

        public State this[int row, int collumn]
        {
            get
            {
                if (row < 0 || row >= ROWS || collumn < 0 || collumn >= COLS)
                    throw new IndexOutOfRangeException();

                return cellState[row, collumn];
            }
            set
            {
                if (row < 0 || row >= ROWS || collumn < 0 || collumn >= COLS)
                    throw new IndexOutOfRangeException();

                cellState[row, collumn] = value;
            }
        }

        public enum State
        {
            Empty,
            P1,
            P2
        }

        private State[,] cellState;

        public const int ROWS = 6;
        public const int COLS = 7;

        public List<Board> children = new List<Board>();
        public int value;
        public bool isPlayer1Turn = true;

        public bool IsColumnFull(int column)
        {
            return cellState[ROWS - 1, column] != State.Empty;
        }

        public void DropPiece(int column, State player)
        {
            for (int i = 0; i < ROWS; i++)
            {
                if (cellState[i, column] == State.Empty)
                {
                    cellState[i, column] = player;
                    break;
                }
            }
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < COLS; i++)
            {
                if (!IsColumnFull(i))
                {
                    return false;
                }
            }
            return true;
        }

        public State IsAligned()
        {
            // Check horizontally
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j <= COLS - 4; j++)
                {
                    if (cellState[i, j] != State.Empty &&
                        cellState[i, j] == cellState[i, j + 1] && 
                        cellState[i, j + 1] == cellState[i, j + 2] && 
                        cellState[i, j + 2] == cellState[i, j + 3])
                    {
                        return cellState[i, j];
                    }
                }
            }

            // Check vertically
            for (int i = 0; i <= ROWS - 4; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (cellState[i, j] != State.Empty && 
                        cellState[i, j] == cellState[i + 1, j] && 
                        cellState[i + 1, j] == cellState[i + 2, j] && 
                        cellState[i + 2, j] == cellState[i + 3, j])
                    {
                        return cellState[i, j];
                    }
                }
            }

            // Check diagonally (bottom-left to top-right)
            for (int i = 0; i <= ROWS - 4; i++)
            {
                for (int j = 0; j <= COLS - 4; j++)
                {
                    if (cellState[i, j] != State.Empty && 
                        cellState[i, j] == cellState[i + 1, j + 1] && 
                        cellState[i + 1, j + 1] == cellState[i + 2, j + 2] && 
                        cellState[i + 2, j + 2] == cellState[i + 3, j + 3])
                    {
                        return cellState[i, j];
                    }
                }
            }

            // Check diagonally (top-left to bottom-right)
            for (int i = 3; i < ROWS; i++)
            {
                for (int j = 0; j <= COLS - 4; j++)
                {
                    if (cellState[i, j] != State.Empty && 
                        cellState[i, j] == cellState[i - 1, j + 1] && 
                        cellState[i - 1, j + 1] == cellState[i - 2, j + 2] && 
                        cellState[i - 2, j + 2] == cellState[i - 3, j + 3])
                    {
                        return cellState[i, j];
                    }
                }
            }

            return State.Empty;
        }
    }
}