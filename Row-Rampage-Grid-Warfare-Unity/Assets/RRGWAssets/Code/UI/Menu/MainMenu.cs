using System.Collections;
using System.Collections.Generic;
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
}
