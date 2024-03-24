using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Menu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Slider volumeSlider;

    private List<Resolution> resolutions = new List<Resolution>
    {
        new Resolution { width = 640, height = 360 },
        new Resolution { width = 854, height = 480 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };

    void Start()
    {
        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);

        foreach (var res in resolutions)
        {
            resolutionOptions.Add(res.width + " x " + res.height);
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        List<string> graphicsOptions = new List<string>(QualitySettings.names);
        graphicsDropdown.AddOptions(graphicsOptions);

        int currentQualityIndex = PlayerPrefs.GetInt("QualityIndex", QualitySettings.GetQualityLevel());
        graphicsDropdown.value = currentQualityIndex;
        graphicsDropdown.RefreshShownValue();
        graphicsDropdown.onValueChanged.AddListener(SetQuality);

        float currentVolumeIndex = PlayerPrefs.GetFloat("VolumeIndex");
        volumeSlider.value = currentVolumeIndex;
        OnVolumeChange(currentVolumeIndex);
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        PlayerPrefs.Save();
    }

    public void OnVolumeChange(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("VolumeIndex", volume);
        PlayerPrefs.Save();
    }

}
