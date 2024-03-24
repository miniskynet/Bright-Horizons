using UnityEngine;

public class Panel_Visibility : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject mainMenu;

    public void displayPanel(){
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void hidePanel(){
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
