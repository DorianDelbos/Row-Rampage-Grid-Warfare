using UnityEngine;

public abstract class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
            panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        CloseAllPanels();
        panel.SetActive(true);
    }
}
