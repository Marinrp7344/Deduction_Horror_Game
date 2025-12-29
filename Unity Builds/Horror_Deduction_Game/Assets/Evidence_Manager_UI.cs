using UnityEngine;
using TMPro;
public class Evidence_Manager_UI : MonoBehaviour
{
    [SerializeField] private GameObject storyView;
    [SerializeField] private GameObject policeReportView;
    [SerializeField] private GameObject videoView;
    [SerializeField] private GameObject audioView;
    [SerializeField] private GameObject imageView;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private TextMeshProUGUI title;

    public void GoToMainMenu()
    {
        storyView.SetActive(false);
        policeReportView.SetActive(false);
        videoView.SetActive(false);
        audioView.SetActive(false);
        imageView.SetActive(false);
        exitButton.SetActive(false);
        buttons.SetActive(true);
        title.text = "Evidence";
    }

    public void ActivateMenu(int index)
    {
        storyView.SetActive(false);
        policeReportView.SetActive(false);
        videoView.SetActive(false);
        audioView.SetActive(false);
        imageView.SetActive(false);
        exitButton.SetActive(true);

        buttons.SetActive(false);

        switch (index)
        {
            case 0:
                storyView.SetActive(true);
                title.text = "Story Evidence";
                break;
            case 1:
                policeReportView.SetActive(true);
                title.text = "Police Report Evidence";
                break;
            case 2:
                videoView.SetActive(true);
                title.text = "Video Evidence";
                break;
            case 3:
                audioView.SetActive(true);
                title.text = "Audio Evidence";
                break;
            case 4:
                imageView.SetActive(true);
                title.text = "Image Evidence";
                break;
        }
    }
}
