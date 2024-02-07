using Algorithm.MinMax;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Board root;

    int countNode = 1;
    int countLeaf = 0;

    int maxDepth = 10;

    private void Start()
    {
        root = new Board();

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

        UpdateUI();
    }

    public Transform buttonList;
    void UpdateUI()
    {
        for (int i = 0; i < root.Rows; i++)
        {
            for (int j = 0; j < root.Columns; j++)
            {
                switch (root[i, j])
                {
                    case Board.State.Empty:
                        buttonList.Find("Button (" + (i * root.Columns + j) + ")").GetComponent<Image>().color = Color.white;
                        break;
                    case Board.State.P1:
                        buttonList.Find("Button (" + (i * root.Columns + j) + ")").GetComponent<Image>().color = Color.red;
                        break;
                    case Board.State.P2:
                        buttonList.Find("Button (" + (i * root.Columns + j) + ")").GetComponent<Image>().color = Color.yellow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
