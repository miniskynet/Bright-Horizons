using UnityEngine;

public class Panel_Visibility : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;

    //hide the main menu and display the options menu
    public void displayPanel()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    //hide the options menu and display the main menu
    public void hidePanel()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
