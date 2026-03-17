using UnityEngine;

public class Story_Evidence_Parent : MonoBehaviour
{
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public int currentIndex;

    public GameObject stories;
    public GameObject storiesParent;
    public AudioSource paperChange;
    public AudioSource paperPickedUp;
    public bool activated;

    public Camera_Animator player;

    public void ActivateStories()
    {
        if (!activated)
        {
            activated = true;
            stories.SetActive(true);
            ChangeStory(0);
            paperPickedUp.pitch = Random.Range(0.75f, 1);
            paperPickedUp.Play();
            player.viewsButtons.DisableButtons();
        }
    }

    public void DeactivateStories()
    {
        player.viewsButtons.ChangeButtons(0);
        stories.SetActive(false);
        activated = false;
    }

    public void ChangeStory(int indexChange)
    {
        currentIndex += indexChange;

        if(currentIndex < 0)
        {
            currentIndex = 2;
        }
        else if(currentIndex > 2)
        {
            currentIndex = 0;
        }

        switch(currentIndex)
        {
            case 0:
                story1.SetActive(true);
                story2.SetActive(false);
                story3.SetActive(false);
                break;
            case 1:
                story1.SetActive(false);
                story2.SetActive(true);
                story3.SetActive(false);
                break;
            case 2:
                story1.SetActive(false);
                story2.SetActive(false);
                story3.SetActive(true);
                break;
        }
        paperChange.pitch = Random.Range(0.75f, 1);
        paperChange.Play();
    }
}
