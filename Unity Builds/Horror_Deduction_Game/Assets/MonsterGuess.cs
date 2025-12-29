using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MonsterGuess : MonoBehaviour
{
    public Director director;
    public Monster_Data monster;
    public Image image;
    public GameObject blockout;
    public bool blockedOut;
    public TextMeshProUGUI monsterName;

    public void UpdateGuess()
    {
        director.UpdateGuess(monster, this);
    }

    public void UnguessButton()
    {
        image.enabled = false;
    }

    public void CrossoutButton()
    {
        blockout.SetActive(true);
    }

    public void DisableCrossoutButton()
    {
        blockout.SetActive(false);
    }

    public void ResetButton()
    {
        image.enabled = false;
        blockout.SetActive(false);
        blockedOut = false;
    }

    public void GuessButton()
    {
        image.enabled = true;
    }
}
