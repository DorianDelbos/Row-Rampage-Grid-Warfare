using Algorithm;
using UnityEngine;

public class RenderingGame3D : RenderingGame
{
    public Transform tokenList;
    public GameObject tokenRedPrefab;
    public GameObject tokenBluePrefab;
    public Transform[] previewList;

    private void Awake()
    {
        boardManager.OnDisplayUpdate += UpdateRenderer;
    }

    public override void UpdateRenderer(Board board)
    {
        foreach (Transform token in tokenList)
        {
            Destroy(token.gameObject);
        }

        for (int i = 0; i < Board.ROWS; i++)
        {
            for (int j = 0; j < Board.COLS; j++)
            {
                InstantiateToken(board[i, j], new Vector2Int(j, i));
            }
        }

        foreach (Transform preview in previewList)
        {
            RemovePreviewToken(preview);
        }
    }

    protected override void InstantiateToken(Board.State state, Vector2Int position)
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

    public void PlacePreviewToken(Transform toPlace)
    {
        GameObject tokenObject;
        if (GameManager.instance.playerTurn == Board.State.P1 && !GameManager.instance.player1.isAI)
        {
            tokenObject = Instantiate(tokenBluePrefab, toPlace.position, new Quaternion(0, 0, 0, 0), toPlace);
            tokenObject.GetComponent<MeshFilter>().mesh = GameManager.instance.player1.token.mesh;
        }
        else if (GameManager.instance.playerTurn == Board.State.P2 && !GameManager.instance.player2.isAI)
        {
            tokenObject = Instantiate(tokenRedPrefab, toPlace.position, new Quaternion(0, 0, 0, 0), toPlace);
            tokenObject.GetComponent<MeshFilter>().mesh = GameManager.instance.player2.token.mesh;
        }
    }

    public void RemovePreviewToken(Transform toPlace)
    {
        foreach (Transform child in toPlace)
        {
            Destroy(child.gameObject);
        }
    }
}
