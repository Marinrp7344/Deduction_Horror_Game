using UnityEngine;
using System.Collections.Generic;
public class ViewsButtons : MonoBehaviour
{
    [SerializeField] private Camera_Animator cameraView;

    //Button 0 is the bottom button
    //Button 1 is the right button
    //Button 2 is the left button
    [SerializeField] private List<bool> activeButtons;
    [SerializeField] private List<GameObject> buttons;

    //Views that buttons are connected to
    //-1 if the button is not Active
    [SerializeField] private int bottomButtonConnectedView;
    [SerializeField] private int rightButtonConnectedView;
    [SerializeField] private int leftButtonConnectedView;

    public void SwitchViews(int buttonIndex)
    {
        int buttonConnectedView = -1;
        switch(buttonIndex)
        {
            case 0:
                buttonConnectedView = bottomButtonConnectedView;
                break;
            case 1:
                buttonConnectedView = rightButtonConnectedView;
                break;
            case 2:
                buttonConnectedView = leftButtonConnectedView;
                break;
        }

        cameraView.SwitchView(buttonConnectedView);
    }

    public void ChangeButtons(int currentView)
    {
        switch(currentView)
        {
            case 0:
                activeButtons[0] = true;
                activeButtons[1] = false;
                activeButtons[2] = false;
                break;
            case 1:
                activeButtons[0] = true;
                activeButtons[1] = true;
                activeButtons[2] = true;
                break;
            case 2:
                activeButtons[0] = true;
                activeButtons[1] = true;
                activeButtons[2] = false;
                break;
            case 3:
                activeButtons[0] = true;
                activeButtons[1] = false;
                activeButtons[2] = true;
                break;
        }

        ActivateButtons();
        ConnectButtons();
    }

    private void ActivateButtons()
    {
        for (int i = 0; i < activeButtons.Count; i++)
        {
            if(activeButtons[i] == true)
            {
                buttons[i].SetActive(true);
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }

    private void ConnectButtons()
    {
        int currentValue = cameraView.GetCurrentView();
        switch(currentValue)
        {
            case 0:
                bottomButtonConnectedView = 1;
                rightButtonConnectedView = -1;
                leftButtonConnectedView = -1;
                break;
            case 1:
                bottomButtonConnectedView = 0;
                rightButtonConnectedView = 2;
                leftButtonConnectedView = 3;
                break;
            case 2:
                bottomButtonConnectedView = 3;
                rightButtonConnectedView = 1;
                leftButtonConnectedView = -1;
                break;
            case 3:
                bottomButtonConnectedView = 2;
                rightButtonConnectedView = -1;
                leftButtonConnectedView = 1;
                break;
        }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(false);
        }
    }
}

