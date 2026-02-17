using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Rendering.LookDev;
using System.Collections.Generic;

public class Story_Evidence : MonoBehaviour
{
    public TextMeshProUGUI story;
    public Evidence_Data evidenceSO;
    public List<StoryChosenTrait> chosenTraits;
 
    //public GameObject storyUI;

    public void InitializePaper()
    {
        string builtStory = "Name: " + evidenceSO.storyName +"\nDate: " + evidenceSO.storyDate + "\n\nTestimonial: " + evidenceSO.storyDescription + "\n\nComments: " + evidenceSO.storyComments;

        story.text = builtStory;
    }

    public void UpdateChosenTraits(TextMeshProUGUI text)
    {
        foreach (StoryChosenTrait trait in chosenTraits)
        {
            if(!trait.occupied)
            {
                trait.OccupyTrait(text);
                break;
            }
        }
    }



}
