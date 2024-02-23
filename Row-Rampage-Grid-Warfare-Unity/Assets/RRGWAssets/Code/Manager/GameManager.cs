using Algorithm;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _i;
    public static GameManager instance
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load("GameManager") as GameObject).GetComponent<GameManager>();
                DontDestroyOnLoad(_i.gameObject);
            }

            return _i;
        }
    }

    public Player player1;
    public Player player2;
    public Board.State playerTurn = Board.State.P1;
    public Board.State lastWinner;

    private void Awake()
    {
        player1 = new Player();
        player2 = new Player();
    }
}
