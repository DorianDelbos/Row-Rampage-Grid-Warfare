using Algorithm.MinMax;
using UnityEngine;

public class RenderingGame3D : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    public Transform tokenList;
    public GameObject tokenRedPrefab;
    public GameObject tokenBluePrefab;

    private void Awake()
    {
        boardManager.OnDisplayUpdate += UpdateRenderer;
    }

    public void UpdateRenderer(Board board)
    {
        foreach (Transform token in tokenList)
        {
            Destroy(token.gameObject);
        }

        for (int i = 0; i < board.Rows; i++)
        {
            for (int j = 0; j < board.Columns; j++)
            {
                InstantiateToken(board[i, j], new Vector2(j, i));
            }
        }
    }

    private void InstantiateToken(Board.State state, Vector2 position)
    {
        GameObject tokenObject;
        if (state == Board.State.P1)
        {
            tokenObject = Instantiate(tokenBluePrefab, tokenList.position + new Vector3(position.x * 3, position.y * 2.5f, 0), new Quaternion(0, 0, 0, 0), tokenList);
            tokenObject.GetComponent<MeshFilter>().mesh = GameManager.instance.player1.token.mesh;
        }
        else if (state == Board.State.P2)
        {
            tokenObject = Instantiate(tokenRedPrefab, tokenList.position + new Vector3(position.x * 3, position.y * 2.5f, 0), new Quaternion(0, 0, 0, 0), tokenList);
            tokenObject.GetComponent<MeshFilter>().mesh = GameManager.instance.player2.token.mesh;
        }
    }
}
