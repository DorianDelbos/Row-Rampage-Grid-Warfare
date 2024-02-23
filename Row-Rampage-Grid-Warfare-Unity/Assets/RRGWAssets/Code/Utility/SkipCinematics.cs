using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkipCinematics : MonoBehaviour
{
    [SerializeField] private GameObject skipPreview;
    [SerializeField] private Image skipImage;

    [SerializeField] private string sceneToLoad = "MainMenu";
    private bool canSkip = true;
    public bool CanSkip
    {
        get => canSkip;
        set
        {
            canSkip = value;

            if (canSkip)
                skipPreview.SetActive(true);
            else
                skipPreview.SetActive(false);
        }
    }

    [SerializeField] private float timeToSkip = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanSkip)
        {
            StartCoroutine(SkipEnumerator());
        }
    }

    private IEnumerator SkipEnumerator()
    {
        float elapsed = 0f;

        while (elapsed < timeToSkip && Input.GetKey(KeyCode.Space) && CanSkip)
        {
            elapsed += Time.deltaTime;
            skipImage.fillAmount = elapsed / timeToSkip;
            yield return null;
        }

        if (elapsed >= timeToSkip)
        {
            SceneSystem.instance.LoadScene(sceneToLoad);
        }
        skipImage.fillAmount = 0;
    }
}
