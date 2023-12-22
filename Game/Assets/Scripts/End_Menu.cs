using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
