using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    private static SceneSystem _i;
    public static SceneSystem instance
    {
        get
        {
            if (_i == null)
                _i = Instantiate(Resources.Load("SceneSystem") as GameObject).GetComponent<SceneSystem>();

            return _i;
        }
    }

    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        if (loadingScreen == null)
        {
            Debug.LogError("No loading screen set");
        }
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        loadingScreen.Enable();
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingScreen.UpdatePercantage(progress);
            yield return null;
        }
    }
}
