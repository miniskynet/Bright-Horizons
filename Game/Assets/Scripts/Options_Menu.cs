using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Menu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;
    public Slider volumeSlider;
    public Dropdown difficultyDropdown;
    public Dropdown learningDropdown;

    private List<Resolution> resolutions = new List<Resolution>
    {
        new Resolution { width = 640, height = 360 },
        new Resolution { width = 854, height = 480 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };

    private List<string> difficulties = new List<string>
    {
        "Easy",
        "Standard",
        "Challenging"
    };

    private List<string> learnings = new List<string>
    {
        "Numbers",
        "Letters",
        "Colors"
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

        // Difficulty Dropdown
        difficultyDropdown.AddOptions(difficulties);
        int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex", 0); // Default to 0 (Easy) if not set
        difficultyDropdown.value = currentDifficultyIndex;
        difficultyDropdown.RefreshShownValue();
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);

        // Learning Dropdown
        learningDropdown.AddOptions(learnings);
        int currentLearningIndex = PlayerPrefs.GetInt("LearningIndex", 0); // Default to 0 (Numbers) if not set
        learningDropdown.value = currentLearningIndex;
        learningDropdown.RefreshShownValue();
        learningDropdown.onValueChanged.AddListener(SetLearning);

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

    public void SetDifficulty(int difficultyIndex)
    {
        // Implement the logic to set the game difficulty based on the difficultyIndex
        // For example, adjust the game parameters or save the index to PlayerPrefs
        PlayerPrefs.SetInt("DifficultyIndex", difficultyIndex);
        PlayerPrefs.Save();
    }

    public void SetLearning(int learningIndex)
    {
        // Implement the logic to set the learning content based on the learningIndex
        // For example, adjust the game parameters or save the index to PlayerPrefs
        PlayerPrefs.SetInt("LearningIndex", learningIndex);
        PlayerPrefs.Save();
    }

}
