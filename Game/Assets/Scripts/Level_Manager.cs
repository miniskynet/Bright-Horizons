using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips;
    public AudioClip finishSoundClip;
    private AudioSource audioSource;
    private static int currentLevel = 0; 
    private int totalLevels = 2;

    private void Start()
    {
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
