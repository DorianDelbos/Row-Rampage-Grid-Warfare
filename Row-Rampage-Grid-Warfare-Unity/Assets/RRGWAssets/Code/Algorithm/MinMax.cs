using UnityEngine;

namespace Algorithm.MinMax
{
    public static class MinMax
    {
        public static int depth = int.MaxValue;

        public static int FindBestMove(Board board, int depth, int alpha, int beta, bool maximazing)
        {
            if (/*depth <= 0 ||*/ board.children.Count == 0) // Noeud feuille
            {
                board.value = board.Evaluate();
                return board.value;
            }

            if (maximazing) // Noeud MAX
            {
                int maxValue = int.MinValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i], --depth, alpha, beta, false);
                    maxValue = Mathf.Max(maxValue, value);
                    //alpha = Mathf.Max(alpha, value);
                    //if (beta <= alpha)
                    //    break;
                }
                board.value = maxValue;
                return maxValue;
            }
            else // Noeud MIN
            {
                int minValue = int.MaxValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i], --depth, alpha, beta, true);
                    minValue = Mathf.Min(minValue, value);
                    //beta = Mathf.Min(beta, value);
                    //if (beta <= alpha)
                    //    break;
                }
                board.value = minValue;
                return minValue;
            }
        }
    }
}
