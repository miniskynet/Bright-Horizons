using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{
    //either quit the game or go back to main menu
    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        //set timescale to default value
        Time.timeScale = 1f;
        Level_Manager.currentLevel = 0;
        SceneManager.LoadScene(0);
    }
}
