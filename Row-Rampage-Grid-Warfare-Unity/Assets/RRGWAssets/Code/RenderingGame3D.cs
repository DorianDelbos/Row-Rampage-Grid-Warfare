using Algorithm.MinMax;
using UnityEngine;

public class RenderingGame3D : MonoBehaviour
{
    public Transform tokenList;
    public GameObject tokenRedPrefab;
    public GameObject tokenYellowPrefab;

    private void Start()
    {
        GameManager.instance.OnDisplayUpdate += UpdateRenderer;
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
                switch (board[i, j])
                {
                    case Board.State.P1:
                        Instantiate(tokenRedPrefab, tokenList.position + new Vector3(j * 3, i * 2.5f, 0), new Quaternion(0, 0, 0, 0), tokenList);
                        break;
                    case Board.State.P2:
                        Instantiate(tokenYellowPrefab, tokenList.position + new Vector3(j * 3, i * 2.5f, 0), new Quaternion(0, 0, 0, 0), tokenList);
                        break;
                    default:
                        break;
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(tokenList.position + new Vector3(j * 3, i * 2.5f, 0), 0.5f);
            }
        }
    }
#endif
}
