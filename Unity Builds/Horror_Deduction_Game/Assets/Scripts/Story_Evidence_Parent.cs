using UnityEngine;

public class Story_Evidence_Parent : MonoBehaviour
{
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public int currentIndex;

    public GameObject stories;
    public GameObject storiesParent;

    public void ActivateStories()
    {
        stories.SetActive(true);
        ChangeStory(0);
    }

    public void DeactivateStories()
    {
        stories.SetActive(false);
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
    }
}
