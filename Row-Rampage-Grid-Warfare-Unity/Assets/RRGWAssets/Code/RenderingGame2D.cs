using Algorithm.MinMax;
using UnityEngine;
using UnityEngine.UI;

public class RenderingGame2D : MonoBehaviour
{
    public Transform buttonList;

    private void Awake()
    {
        GameManager.instance.OnDisplayUpdate += UpdateUI;
    }

    void UpdateUI(Board board)
    {
        for (int i = 0; i < board.Rows; i++)
        {
            for (int j = 0; j < board.Columns; j++)
            {
                Image image = buttonList.GetChild(i * board.Columns + j).GetComponent<Image>();

                switch (board[i, j])
                {
                    case Board.State.Empty:
                        image.color = Color.white;
                        break;
                    case Board.State.P1:
                        image.color = Color.red;
                        break;
                    case Board.State.P2:
                        image.color = Color.yellow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
