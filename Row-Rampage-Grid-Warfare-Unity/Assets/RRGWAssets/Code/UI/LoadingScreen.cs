using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string[] tips;
    private int currentTip;

    [SerializeField] private TMP_Text tipsMesh;
    [SerializeField] private TMP_Text percantageMesh;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        StartCoroutine(nextTip());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator nextTip()
    {
        currentTip = Random.Range(0, tips.Length);
        while (true)
        {
            tipsMesh.text = tips[currentTip++ % tips.Length];
            yield return new WaitForSeconds(5f);
        }
    }

    public void Enable()
    {
        UpdatePercantage(0f);
        gameObject.SetActive(true);
    }

    public void UpdatePercantage(float value)
    {
        percantageMesh.text = (value * 100f).ToString() + " %";
        loadingSlider.value = value;
    }
}
