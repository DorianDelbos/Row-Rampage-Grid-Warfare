using Algorithm.MinMax;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Board root;

    int countNode = 1;
    int countLeaf = 0;

    [Range(1, 10)] public int maxDepth = 10;

    public System.Action<Board> OnDisplayUpdate;
    public System.Action<Board.State> OnWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        root = new Board();

        OnWin += state => { Debug.Log(state + " win !"); };

        PlayAI();
    }

    public void GenerateBoardTree(Board board, int depth = 0)
    {
        if (board.IsAligned() != Board.State.Empty || depth >= maxDepth)
        {
            countLeaf++;
            return;
        }

        for (int j = 0; j < board.Columns; j++)
        {
            if (!board.IsColumnFull(j))
            {
                Board newBoard = new Board(board);

                newBoard.DropPiece(j, board.isMaxTurn ? Board.State.P1 : Board.State.P2);
                newBoard.isMaxTurn = !board.isMaxTurn;

                countNode++;

                GenerateBoardTree(newBoard, ++depth);
                board.children.Add(newBoard);
            }
        }

        if (board.children.Count == 0) // Noeud Feuille
        {
            countLeaf++;
        }
    }

    public void Play(int column)
    {
        if (root.IsColumnFull(column) || root.IsBoardFull())
            return;

        root.DropPiece(column, Board.State.P2);

        OnDisplayUpdate?.Invoke(root);
        if (CheckWin())
            return;

        PlayAI();
    }

    public void PlayAI()
    {
        root.children.Clear();

        root.isMaxTurn = true;
        GenerateBoardTree(root);

        int minMax = MinMax.FindBestMove(root);
        root = root.children
            .Where(x => x.value == minMax)
            .OrderBy(_ => Random.Range(0, int.MaxValue))
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
