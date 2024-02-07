using System.Collections.Generic;
using System;
using UnityEngine;

namespace Algorithm.MinMax
{
    public class Board
    {
        public Board()
        {
            rows = 6;
            columns = 7;

            cellState = new State[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
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
                if (row < 0 || row >= rows || collumn < 0 || collumn >= columns)
                    throw new IndexOutOfRangeException();

                return cellState[row, collumn];
            }
            set
            {
                if (row < 0 || row >= rows || collumn < 0 || collumn >= columns)
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

        private int rows;
        private int columns;

        public int Rows => rows;
        public int Columns => columns;

        public List<Board> children = new List<Board>();
        public int value;
        public bool isMaxTurn = true;

        public bool IsColumnFull(int column)
        {
            return cellState[Rows - 1, column] != State.Empty;
        }

        public void DropPiece(int column, State player)
        {
            for (int i = 0; i < Rows; i++)
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
            for (int i = 0; i < Columns; i++)
            {
                if (!IsColumnFull(i))
                {
                    return false;
                }
            }
            return true;
        }

        public int Evaluate()
        {
            State state = IsAligned();

            switch (state)
            {
                case State.P1:
                    return 100;
                case State.P2:
                    return -100;
            }

            return 0;
        }

        public State IsAligned()
        {
            // Check horizontally
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j <= Columns - 4; j++)
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
            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 0; j < Columns; j++)
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
            for (int i = 0; i <= Rows - 4; i++)
            {
                for (int j = 0; j <= Columns - 4; j++)
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
            for (int i = 3; i < Rows; i++)
            {
                for (int j = 0; j <= Columns - 4; j++)
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

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = Rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < Columns; j++)
                {
                    switch (this[i, j])
                    {
                        case State.Empty:
                            result += "_";
                            break;
                        case State.P1:
                            result += "X";
                            break;
                        case State.P2:
                            result += "O";
                            break;
                    }
                    result += " ";
                }
                result += "\n";
            }

            return result;
        }
    }
}