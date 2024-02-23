using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MenuHandler
{
    private bool pauseActive => panels.Any(x => x.activeSelf);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseActive)
            {
                ClosePause();
            }
            else
            {
                OpenPause();
            }
        }
    }

    public void OpenPause()
    {
        OpenPanel(panels.FirstOrDefault());
        Time.timeScale = 0.0f;
    }

    public void ClosePause()
    {
        CloseAllPanels();
        Time.timeScale = 1.0f;
    }

    public void BackMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneSystem.instance.LoadScene("MainMenu");
    }

    public void QuitGame()
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
