using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Menu : MonoBehaviour
{
    public GameObject menuBG;
    public GameObject optionsMenu;
    public GameObject buttons;
    public bool inOptionsMenu;
    public bool inMenu;
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OptionsButton()
    {
        optionsMenu.SetActive(true);
        buttons.SetActive(false);
        inOptionsMenu = true;
    }

    public void ExitMenu()
    {
        if(!inOptionsMenu)
        {
            menuBG.SetActive(false);
            inMenu = false;
        }
        else
        {
            optionsMenu.SetActive(false);
            buttons.SetActive(true);
            inOptionsMenu = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnToggleMenu()
    {
        if(!inMenu)
        {
            inMenu = true;
            menuBG.SetActive(true);
        }
        else
        { 
            ExitMenu();
        }
    }
}
