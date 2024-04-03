//The initial code was generated with assistance from ChatGPT, an AI developed by OpenAI (OpenAI, 2023).
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

    //create lists to store the resolutions, 
    //difficulty levels and learning preferences

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

        //populate the dropdown with the resolution values
        //and set the default value as the saved user preference
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        //get the graphical presets from the unity editor and populate the dropdown
        List<string> graphicsOptions = new List<string>(QualitySettings.names);
        graphicsDropdown.AddOptions(graphicsOptions);
        int currentQualityIndex = PlayerPrefs.GetInt("QualityIndex", QualitySettings.GetQualityLevel());
        graphicsDropdown.value = currentQualityIndex;
        graphicsDropdown.RefreshShownValue();
        graphicsDropdown.onValueChanged.AddListener(SetQuality);

        //change the master volume with the slider movement
        float currentVolumeIndex = PlayerPrefs.GetFloat("VolumeIndex");
        volumeSlider.value = currentVolumeIndex;
        OnVolumeChange(currentVolumeIndex);
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

        //populat the dropdown with difficulty levels
        difficultyDropdown.AddOptions(difficulties);
        //set to easy by default
        int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex", 0);
        difficultyDropdown.value = currentDifficultyIndex;
        difficultyDropdown.RefreshShownValue();
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);

        //populat the dropdown with learning resource options
        learningDropdown.AddOptions(learnings);
        //set to numbers by default
        int currentLearningIndex = PlayerPrefs.GetInt("LearningIndex", 0);
        learningDropdown.value = currentLearningIndex;
        learningDropdown.RefreshShownValue();
        learningDropdown.onValueChanged.AddListener(SetLearning);

    }

    //following set methods are called upon when the user 
    //changes a setting from the options menu
    //the selected option is saved to the PlayerPrefs file

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
        PlayerPrefs.SetInt("DifficultyIndex", difficultyIndex);
        PlayerPrefs.Save();
    }

    public void SetLearning(int learningIndex)
    {
        PlayerPrefs.SetInt("LearningIndex", learningIndex);
        PlayerPrefs.Save();
    }

}
