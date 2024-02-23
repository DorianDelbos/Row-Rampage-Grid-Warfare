using UnityEngine;

public class WinnerManager : MonoBehaviour
{
    [SerializeField] private GameObject winnerToken;
    [SerializeField] private GameObject loserToken;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    private void Start()
    {
        if (GameManager.instance.lastWinner == Algorithm.Board.State.P1)
        {
            winnerToken.GetComponent<MeshFilter>().mesh = GameManager.instance.player1.token.mesh;
            winnerToken.GetComponent<MeshRenderer>().material = blue;

            loserToken.GetComponent<MeshFilter>().mesh = GameManager.instance.player2.token.mesh;
            loserToken.GetComponent<MeshRenderer>().material = red;
        }
        else if (GameManager.instance.lastWinner == Algorithm.Board.State.P2)
        {
            winnerToken.GetComponent<MeshFilter>().mesh = GameManager.instance.player2.token.mesh;
            winnerToken.GetComponent<MeshRenderer>().material = red;

            loserToken.GetComponent<MeshFilter>().mesh = GameManager.instance.player1.token.mesh;
            loserToken.GetComponent<MeshRenderer>().material = blue;
        }
        else
        {
            SceneSystem.instance.LoadScene("MainMenu");
        }
    }
}
