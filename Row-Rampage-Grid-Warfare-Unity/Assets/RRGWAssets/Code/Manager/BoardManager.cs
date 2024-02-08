using Algorithm;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Board root;

    private int maxDepth = 16;

    public System.Action<Board> OnDisplayUpdate;
    public System.Action<Board.State> OnWin;

    private void Start()
    {
        root = new Board();

        OnWin += state => { Debug.Log(state + " win !"); };

        PlayAI();
    }

    public void GenerateBoardTree(Board board, int depth)
    {
        if (depth <= 0 || board.IsAligned() != Board.State.Empty || board.IsBoardFull())
        {
            return;
        }

        for (int j = 0; j < Board.COLS; j++)
        {
            if (!board.IsColumnFull(j))
            {
                Board newBoard = new Board(board);
                newBoard.DropPiece(j, board.isPlayer1Turn ? Board.State.P2 : Board.State.P1);
                newBoard.isPlayer1Turn = !board.isPlayer1Turn;

                GenerateBoardTree(newBoard, --depth);
                board.children.Add(newBoard);
            }
        }
    }

    public void Play(int column)
    {
        if (root.IsColumnFull(column) || root.IsBoardFull())
            return;

        root.DropPiece(column, root.isPlayer1Turn ? Board.State.P2 : Board.State.P1);

        OnDisplayUpdate?.Invoke(root);
        if (CheckWin())
            return;

        PlayAI();
    }

    public void PlayAI()
    {
        root.children.Clear();

        root.isPlayer1Turn = true;
        GenerateBoardTree(root, maxDepth);

        int minMax = MinMax.FindBestMove(root, int.MinValue, int.MaxValue, true);
        root = root.children
            .Where(x => x.value == minMax)
            //.OrderBy(_ => Random.Range(0, int.MaxValue))
            .First();

        OnDisplayUpdate?.Invoke(root);
        if (CheckWin())
            return;
    }

    private bool CheckWin()
    {
        Board.State state = root.IsAligned();
        if (root.IsBoardFull() || state != Board.State.Empty)
        {
            OnWin?.Invoke(state);

            return true;
        }
        return false;
    }
}
