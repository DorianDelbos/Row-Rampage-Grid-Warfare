using Algorithm;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Board root;

    private bool gameStarted = false;

    public System.Action<Board> OnDisplayUpdate;
    public System.Action<Board.State> OnWin;

    private Board.State playerTurn
    {
        get => GameManager.instance.playerTurn;
        set => GameManager.instance.playerTurn = value;
    }

    private bool player1IsAI => GameManager.instance.player1.isAI;
    private bool player2IsAI => GameManager.instance.player2.isAI;

    [SerializeField] private TMP_Text turnText;

    public void GameStart()
    {
        gameStarted = true;
        root = new Board();
        root.isPlayer1Turn = false;
        UpdateTextTurn();

        if (player1IsAI)
            StartCoroutine(PlayAI());
    }

    public void Play(int column)
    {
        if (!gameStarted)
            return;

        if ((playerTurn == Board.State.P1 && player1IsAI) ||
            (playerTurn == Board.State.P2 && player2IsAI))
            return;

        if (root.IsColumnFull(column) || root.IsBoardFull())
            return;

        root.DropPiece(column, playerTurn);
        AudioManager.instance.PlayAudio(GameAssets.instance.playMoveSound);

        NextPlayer();
        OnDisplayUpdate?.Invoke(root);

        if (CheckWin())
        {
            // On Win
            SceneSystem.instance.LoadScene("Winner");
            return;
        }

        if ((playerTurn == Board.State.P1 && player1IsAI) ||
            (playerTurn == Board.State.P2 && player2IsAI))
        {
            StartCoroutine(PlayAI());
        }
    }

    public IEnumerator PlayAI()
    {
        yield return new WaitForSeconds(1f);

        root.children.Clear();

        int depth = playerTurn == Board.State.P1 ? GameManager.instance.player1.difficulty : GameManager.instance.player2.difficulty;
        MinMax.GenerateBoardTree(ref root, depth);
        int minMax = MinMax.Minimax(root, depth, int.MinValue, int.MaxValue, true);
        root = root.children
            .Where(x => x.value == minMax)
            //.OrderBy(_ => Random.Range(0, int.MaxValue))
            .First();

        AudioManager.instance.PlayAudio(GameAssets.instance.playMoveSound);
        NextPlayer();
        OnDisplayUpdate?.Invoke(root);

        if (CheckWin())
        {
            // On Win
            SceneSystem.instance.LoadScene("Winner");
        }
        else if ((playerTurn == Board.State.P1 && player1IsAI) ||
            (playerTurn == Board.State.P2 && player2IsAI))
        {
            StartCoroutine(PlayAI());
        }
    }

    public void NextPlayer()
    {
        root.isPlayer1Turn = playerTurn == Board.State.P1;
        playerTurn = playerTurn == Board.State.P1 ? Board.State.P2 : Board.State.P1;

        UpdateTextTurn();
    }

    private void UpdateTextTurn()
    {
        if (playerTurn == Board.State.P1)
            turnText.text = "<color=blue>" + GameManager.instance.player1.name + "</color>'s turn !";
        else
            turnText.text = "<color=red>" + GameManager.instance.player2.name + "</color>'s turn !";
    }

    private bool CheckWin()
    {
        Board.State state = root.IsAligned();
        if (root.IsBoardFull() || state != Board.State.Empty)
        {
            OnWin?.Invoke(state);
            GameManager.instance.lastWinner = state;
            return true;
        }
        return false;
    }
}
