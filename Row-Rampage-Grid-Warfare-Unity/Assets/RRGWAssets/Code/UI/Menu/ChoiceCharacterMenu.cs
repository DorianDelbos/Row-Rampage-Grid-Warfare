using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCharacterMenu : MenuHandler
{
    [SerializeField] private Transform[] customCharacterList;
    [SerializeField] private Button customButtonPrefab;
    [SerializeField] private MeshFilter TokenBlue;
    [SerializeField] private MeshFilter TokenRed;

    [SerializeField] private TMP_InputField pseudoPlayer1;
    [SerializeField] private TMP_InputField pseudoPlayer2;

    [SerializeField] private TMP_Text isAiPlayer1;
    [SerializeField] private TMP_Text isAiPlayer2;

    [SerializeField] private TMP_Dropdown difficulty1;
    [SerializeField] private TMP_Dropdown difficulty2;

    private enum Difficulty
    {
        Easy = 3,
        Medium = 5,
        Hard = 7
    }

    private void Start()
    {
        Token startToken = GameAssets.instance.tokens.First();
        GameManager.instance.player1.token = startToken;
        TokenBlue.mesh = startToken.mesh;
        GameManager.instance.player2.token = startToken;
        TokenRed.mesh = startToken.mesh;

        // Init UI
        for (int i = 0; i < customCharacterList.Length; i++)
        {
            foreach (Token token in GameAssets.instance.tokens)
            {
                Button newButton = Instantiate(customButtonPrefab, customCharacterList[i]);
                newButton.transform.GetChild(0).GetComponent<RawImage>().texture = token.texture;

                if (i == 0)
                {
                    newButton.onClick.AddListener((UnityEngine.Events.UnityAction)(() => {
                        GameManager.instance.player1.token = token;
                        TokenBlue.mesh = token.mesh;
                    }));
                }
                else
                {
                    newButton.onClick.AddListener((UnityEngine.Events.UnityAction)(() => {
                        GameManager.instance.player2.token = token;
                        TokenRed.mesh = token.mesh;
                    }));
                }
            }
        }
    }

    public void StartGame()
    {
        GameManager.instance.player1.name = pseudoPlayer1.text;
        GameManager.instance.player2.name = pseudoPlayer2.text;

        SetDifficulty(difficulty1.value, true);
        SetDifficulty(difficulty2.value, false);

        SceneSystem.instance.LoadScene("InGame");
    }

    public void BackMainMenu()
    {
        SceneSystem.instance.LoadScene("MainMenu");
    }

    public void SetPlayerAI(bool isPlayer1)
    {
        if (isPlayer1)
        {
            GameManager.instance.player1.isAI = !GameManager.instance.player1.isAI;
            difficulty1.interactable = GameManager.instance.player1.isAI;

            if (GameManager.instance.player1.isAI)
                isAiPlayer1.text = "Join as Player";
            else
                isAiPlayer1.text = "Join as AI";
        }
        else
        {
            GameManager.instance.player2.isAI = !GameManager.instance.player2.isAI;
            difficulty2.interactable = GameManager.instance.player2.isAI;

            if (GameManager.instance.player2.isAI)
                isAiPlayer2.text = "Join as Player";
            else
                isAiPlayer2.text = "Join as AI";
        }
    }

    public void SetDifficulty(int ind, bool ply1)
    {
        if (ply1)
        {
            switch (ind)
            {
                case 0:
                    GameManager.instance.player1.difficulty = 3;
                    break;
                case 1:
                    GameManager.instance.player1.difficulty = 5;
                    break;
                case 2:
                    GameManager.instance.player1.difficulty = 7;
                    break;
                default:
                    break;
            }
        }
        else
        {

            switch (ind)
            {
                case 0:
                    GameManager.instance.player2.difficulty = 3;
                    break;
                case 1:
                    GameManager.instance.player2.difficulty = 5;
                    break;
                case 2:
                    GameManager.instance.player2.difficulty = 7;
                    break;
                default:
                    break;
            }
        }
    }
}
