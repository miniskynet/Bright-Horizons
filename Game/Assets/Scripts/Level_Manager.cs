using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips;
    public AudioClip finishSoundClip;
    private AudioSource audioSource;
    public static int currentLevel = 0; 
    private int totalLevels;

    private void Start()
    {
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
            Invoke("CompleteLevel", 2f);
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
}
