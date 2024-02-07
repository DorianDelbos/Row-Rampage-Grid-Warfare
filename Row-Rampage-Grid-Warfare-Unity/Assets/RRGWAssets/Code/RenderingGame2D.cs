using Algorithm.MinMax;
using UnityEngine;
using UnityEngine.UI;

public class RenderingGame2D : MonoBehaviour
{
    public Transform buttonList;

    private void Start()
    {
        GameManager.instance.OnDisplayUpdate += UpdateUI;
    }

    void UpdateUI(Board board)
    {
        for (int i = 0; i < board.Rows; i++)
        {
            for (int j = 0; j < board.Columns; j++)
            {
                switch (board[i, j])
                {
                    case Board.State.Empty:
                        buttonList.Find("Button (" + (i * board.Columns + j) + ")").GetComponent<Image>().color = Color.white;
                        break;
                    case Board.State.P1:
                        buttonList.Find("Button (" + (i * board.Columns + j) + ")").GetComponent<Image>().color = Color.red;
                        break;
                    case Board.State.P2:
                        buttonList.Find("Button (" + (i * board.Columns + j) + ")").GetComponent<Image>().color = Color.yellow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
