using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public void StartGame()
    {
        //load the main menu once the game starts
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
