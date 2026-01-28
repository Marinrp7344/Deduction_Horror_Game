using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject compendium;
    public bool onMainMenu;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitMenu()
    {
        if(!onMainMenu)
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            compendium.SetActive(false);
            onMainMenu = true;
        }
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        compendium.SetActive(false);

        onMainMenu = false;
    }

    public void Compendium()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        compendium.SetActive(true);
        onMainMenu = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
