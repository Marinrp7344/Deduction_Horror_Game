using UnityEngine;
using UnityEngine.UI;
public class CompendiumZoomImage : MonoBehaviour
{
    public Image displayImage;
    public Image zoomedImage;
    public GameObject zoom;
    public void DisplayImage()
    {
        zoomedImage.sprite = displayImage.sprite;
        zoom.SetActive(true);
    }

    public void CloseImage()
    {
        zoom.SetActive(false);
    }

}
