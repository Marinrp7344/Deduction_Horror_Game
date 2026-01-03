using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Story_Evidence : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Evidence_Data evidenceSO;

    public GameObject storyUI;

    public void InitializePaper()
    {
        title.text = evidenceSO.storyTitle;
        description.text = evidenceSO.storyDescription;
    }

    public void ClosePaperMenu()
    {
        storyUI.SetActive(false);
    }

    public void OpenPaperMenu()
    {
        storyUI.SetActive(true);
    }


}
