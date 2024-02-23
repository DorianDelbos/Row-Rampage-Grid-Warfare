using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Collections.Generic;

public class SettingsMenu : MenuHandler
{
    private Bus mainBus;
    private Bus musicBus;
    private Bus soundEffectBus;

    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;

    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Start()
    {
        // Bus
        mainBus = RuntimeManager.GetBus("bus:/Main");
        musicBus = RuntimeManager.GetBus("bus:/Main/Music");
        soundEffectBus = RuntimeManager.GetBus("bus:/Main/SoundEffect");

        // Sliders
        mainBus.getVolume(out float mainBusVolume);
        mainSlider.value = mainBusVolume * 100f;

        musicBus.getVolume(out float musicBusVolume);
        musicSlider.value = musicBusVolume * 100f;

        soundEffectBus.getVolume(out float soundEffectBusVolume);
        soundEffectSlider.value = soundEffectBusVolume * 100;

        // Toggle
        fullscreenToggle.isOn = Screen.fullScreen;

        // Dropdown
        resolutionDropdown.options.Clear();
        qualityDropdown.options.Clear();

        qualityDropdown.AddOptions(QualitySettings.names.ToList());

        // Resolution
        List<string> options = new List<string>();
        int curResolutionIndex = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            string option = Screen.resolutions[i].width + " x " + Screen.resolutions[i].height + " @ " + Screen.resolutions[i].refreshRateRatio + "hz";
            options.Add(option);

            if (Screen.resolutions[i].width == Screen.width &&
                Screen.resolutions[i].height == Screen.height &&
                Screen.resolutions[i].refreshRateRatio.ToString() == Screen.currentResolution.refreshRateRatio.ToString())
            {
                curResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curResolutionIndex;
        resolutionDropdown.interactable = !Screen.fullScreen;
    }

    public void SetMainVolume(float volume)
    {
        mainBus.setVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicBus.setVolume(volume);
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectBus.setVolume(volume);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;
        resolutionDropdown.interactable = !isFullscreen;
    }
}
