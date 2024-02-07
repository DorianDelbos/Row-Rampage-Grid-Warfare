namespace Algorithm.MinMax
{
    public static class MinMax
    {
        public static int FindBestMove(Board board)
        {
            if (board.children.Count == 0) // Noeud feuille
            {
                board.value = board.Evaluate();
                return board.value;
            }

            if (board.isMaxTurn) // Noeud MAX
            {
                int maxValue = int.MinValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i]);
                    if (value > maxValue)
                    {
                        maxValue = value;
                    }
                }
                board.value = maxValue;
                return maxValue;
            }
            else // Noeud MIN
            {
                int minValue = int.MaxValue;
                for (int i = 0; i < board.children.Count; i++)
                {
                    int value = FindBestMove(board.children[i]);
                    if (value < minValue)
                    {
                        minValue = value;
                    }
                }
                board.value = minValue;
                return minValue;
            }
        }
    }
}
