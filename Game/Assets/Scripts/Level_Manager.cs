using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips;
    public AudioClip finishSoundClip;
    private AudioSource audioSource;
    public static int currentLevel = 0; 
    private int totalLevels;
    [SerializeField] private Text currentLevelText;
    public Text levelCompletedText;

    private void Start()
    {
        currentLevelText.text = "Current Level : " + (currentLevel + 1);
        int currentDifficultyIndex = PlayerPrefs.GetInt("DifficultyIndex");
        if(currentDifficultyIndex == 0){
            totalLevels = 3;
        } else if (currentDifficultyIndex == 1){
            totalLevels = 5;
        } else if (currentDifficultyIndex == 2){
            totalLevels = 10;
        }
        audioSource = GetComponent<AudioSource>();
        PlayRandomBackgroundMusic();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            audioSource.PlayOneShot(finishSoundClip);
            StartCoroutine(DisplayLevelCompletedText());
            Invoke("CompleteLevel", 3f);
        }
    }

    private void CompleteLevel()
    {
        currentLevel++;
        if (currentLevel >= totalLevels)
        {
            currentLevel=0;
            SceneManager.LoadScene("End_Screen"); 
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            PlayRandomBackgroundMusic();
        }
    }

    private void PlayRandomBackgroundMusic()
    {
        int randomIndex = Random.Range(0, backgroundMusicClips.Length);
            audioSource.clip = backgroundMusicClips[randomIndex];
            audioSource.Play();
    }

    private IEnumerator DisplayLevelCompletedText()
    {
        levelCompletedText.gameObject.SetActive(true); // Enable the text element
        yield return new WaitForSeconds(2f); // Wait for the specified duration
        levelCompletedText.gameObject.SetActive(false); // Disable the text element
    }
}
