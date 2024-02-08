using Algorithm;
using UnityEngine;
using UnityEngine.UI;

public class RenderingGame2D : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    public Transform buttonList;

    private void Awake()
    {
        boardManager.OnDisplayUpdate += UpdateUI;
    }

    void UpdateUI(Board board)
    {
        for (int i = 0; i < Board.ROWS; i++)
        {
            for (int j = 0; j < Board.COLS; j++)
            {
                Image image = buttonList.GetChild(i * Board.COLS + j).GetComponent<Image>();

                switch (board[i, j])
                {
                    case Board.State.Empty:
                        image.color = Color.white;
                        break;
                    case Board.State.P1:
                        image.color = Color.blue;
                        break;
                    case Board.State.P2:
                        image.color = Color.red;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
