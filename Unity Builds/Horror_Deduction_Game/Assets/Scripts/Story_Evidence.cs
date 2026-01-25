using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Story_Evidence : MonoBehaviour
{
    public TextMeshProUGUI story;
    public Evidence_Data evidenceSO;

    //public GameObject storyUI;

    public void InitializePaper()
    {
        string builtStory = "Name: " + evidenceSO.storyName +"\nDate: " + evidenceSO.storyDate + "\n\nTestimonial: " + evidenceSO.storyDescription + "\n\nComments: " + evidenceSO.storyComments;

        story.text = builtStory;
    }



}
