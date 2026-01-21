using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject buttons;
    public bool inOptionsMenu;
    public void GoToMainMenu()
    {

    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
        buttons.SetActive(false);
    }

    public void ExitMenu()
    {
        if(!inOptionsMenu)
        {
            menu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(false);
            buttons.SetActive(true);
        }
    }

    public void ExitGame()
    {

    }
}
