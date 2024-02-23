using UnityEngine;

namespace Algorithm
{
    public static class MinMax
    {
        public static int Minimax(Board node, int depth, int alpha, int beta, bool maximazing)
        {
            if (node.children.Count == 0 || depth == 0) // Noeud feuille
            {
                node.value = Evaluate(node);
                return node.value;
            }

            if (maximazing) // Noeud MAX
            {
                int maxValue = int.MinValue;
                for (int i = 0; i < node.children.Count; i++)
                {
                    int value = Minimax(node.children[i], depth - 1, alpha, beta, false);
                    maxValue = Mathf.Max(maxValue, value);
                    alpha = Mathf.Max(alpha, value);
                    if (beta <= alpha)
                        break;
                }
                node.value = maxValue;
                return maxValue;
            }
            else // Noeud MIN
            {
                int minValue = int.MaxValue;
                for (int i = 0; i < node.children.Count; i++)
                {
                    int value = Minimax(node.children[i], depth - 1, alpha, beta, true);
                    minValue = Mathf.Min(minValue, value);
                    beta = Mathf.Min(beta, value);
                    if (beta <= alpha)
                        break;
                }
                node.value = minValue;
                return minValue;
            }
        }

        public static void GenerateBoardTree(ref Board node, int depth)
        {
            if (depth <= 0 || node.IsAligned() != Board.State.Empty || node.IsBoardFull())
            {
                return;
            }

            for (int j = 0; j < Board.COLS; j++)
            {
                if (!node.IsColumnFull(j))
                {
                    Board newBoard = new Board(node);
                    newBoard.DropPiece(j, node.isPlayer1Turn ? Board.State.P2 : Board.State.P1);
                    newBoard.isPlayer1Turn = !node.isPlayer1Turn;

                    GenerateBoardTree(ref newBoard, depth - 1);
                    node.children.Add(newBoard);
                }
            }
        }

        public static int Evaluate(Board node)
        {
            int score = 0;

            for (int row = 0; row < Board.ROWS; row++)
            {
                for (int col = 0; col <= Board.COLS - 4; col++)
                {
                    Board.State[] window = new Board.State[4];
                    for (int i = 0; i < 4; i++)
                    {
                        window[i] = node[row, col + i];
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
                        window[i] = node[row + i, col];
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
                        window[i] = node[row + i, col + i];
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
                        window[i] = node[row + i, col - i];
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
                if (token == GameManager.instance.playerTurn)
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
                score += 1000;
            }
            if (opponentTokens == 3 && playerTokens == 1)
            {
                score += 500;
            }
            if (playerTokens == 3)
            {
                score += 5;
            }
            if (playerTokens == 2)
            {
                score += 1;
            }

            if (opponentTokens == 4)
            {
                score -= 1000;
            }
            if (playerTokens == 3 && opponentTokens == 1)
            {
                score -= 500;
            }
            if (opponentTokens == 3)
            {
                score -= 5;
            }
            if (opponentTokens == 2)
            {
                score -= 1;
            }

            return score;
        }
    }
}
