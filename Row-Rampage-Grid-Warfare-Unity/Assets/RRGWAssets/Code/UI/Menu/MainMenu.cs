using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuHandler
{
    [SerializeField] private Button[] buttonToDisable;

    public void SetActiveButton(bool enable)
    {
        foreach (var button in buttonToDisable)
        {
            button.interactable = enable;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif

        Application.Quit();
    }
}
