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
                InstantiateToken(board[i, j], new Vector2Int(i, j));
            }
        }
    }

    private void InstantiateToken(Board.State state, Vector2Int position)
    {
        RawImage image = buttonList.GetChild(position.x * Board.COLS + position.y).GetComponent<RawImage>();

        if (state == Board.State.P1)
        {
            image.color = Color.blue;
            image.texture = GameManager.instance.player1.token.texture;
        }
        else if (state == Board.State.P2)
        {
            image.color = Color.red;
            image.texture = GameManager.instance.player2.token.texture;
        }
    }
}
