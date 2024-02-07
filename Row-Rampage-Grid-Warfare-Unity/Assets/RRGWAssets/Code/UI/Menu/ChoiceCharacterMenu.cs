using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCharacterMenu : MenuHandler
{
    public void StartGame()
    {
        //SceneSystem.instance.LoadScene("ChoiceCharacter");
    }

    public void BackMainMenu()
    {
        SceneSystem.instance.LoadScene("MainMenu");
    }
}
