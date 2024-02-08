using UnityEngine;

namespace Algorithm
{
    public static class MinMax
    {
        public static int FindBestMove(Board board, int alpha, int beta, bool maximazing)
        {
            if (board.children.Count == 0) // Noeud feuille
            {
                board.value = Evaluate(board);
                return board.value;
            }

            if (maximazing) // Noeud MAX
            {
                int maxValue = int.MinValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i], alpha, beta, false);
                    maxValue = Mathf.Max(maxValue, value);
                    alpha = Mathf.Max(alpha, value);
                    if (beta <= alpha)
                        break;
                }
                board.value = maxValue;
                return maxValue;
            }
            else // Noeud MIN
            {
                int minValue = int.MaxValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i], alpha, beta, true);
                    minValue = Mathf.Min(minValue, value);
                    beta = Mathf.Min(beta, value);
                    if (beta <= alpha)
                        break;
                }
                board.value = minValue;
                return minValue;
            }
        }

        public static int Evaluate(Board board)
        {
            int score = 0;

            for (int row = 0; row < Board.ROWS; row++)
            {
                for (int col = 0; col <= Board.COLS - 4; col++)
                {
                    Board.State[] window = new Board.State[4];
                    for (int i = 0; i < 4; i++)
                    {
                        window[i] = board[row, col + i];
                    }
                    score += EvaluateWindow(window);
                }
            }

            for (int col = 0; col < Board.COLS; col++)
            {
                for (int row = 0; row <= Board.ROWS - 4; row++)
                {
                    Board.State[] window = new Board.State[4];
                    for (int i = 0; i < 4; i++)
                    {
                        window[i] = board[row + i, col];
                    }
                    score += EvaluateWindow(window);
                }
            }

            for (int row = 0; row <= Board.ROWS - 4; row++)
            {
                for (int col = 0; col <= Board.COLS - 4; col++)
                {
                    Board.State[] window = new Board.State[4];
                    for (int i = 0; i < 4; i++)
                    {
                        window[i] = board[row + i, col + i];
                    }
                    score += EvaluateWindow(window);
                }
            }

            for (int row = 0; row <= Board.ROWS - 4; row++)
            {
                for (int col = 3; col < Board.COLS; col++)
                {
                    Board.State[] window = new Board.State[4];
                    for (int i = 0; i < 4; i++)
                    {
                        window[i] = board[row + i, col - i];
                    }
                    score += EvaluateWindow(window);
                }
            }

            return score;
        }

        private static int EvaluateWindow(Board.State[] window)
        {
            int score = 0;
            int playerTokens = 0;
            int opponentTokens = 0;

            foreach (Board.State token in window)
            {
                if (token == Board.State.P2)
                {
                    playerTokens++;
                }
                else if (token != 0)
                {
                    opponentTokens++;
                }
            }

            if (playerTokens == 4)
            {
                score += 100;
            }
            else if (playerTokens == 3 && opponentTokens == 0)
            {
                score += 5;
            }
            else if (playerTokens == 2 && opponentTokens == 0)
            {
                score += 2;
            }

            if (playerTokens == 4)
            {
                score -= 100;
            }
            else if (opponentTokens == 3 && playerTokens == 0)
            {
                score -= 5;
            }
            else if (opponentTokens == 2 && playerTokens == 0)
            {
                score -= 2;
            }

            return score;
        }
    }
}
