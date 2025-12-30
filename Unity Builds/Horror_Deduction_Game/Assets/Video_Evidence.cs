using UnityEngine;
using UnityEngine.Video;

public class Video_Evidence : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Camera_Animator cameraAnimator;
    

    public void PlayVideo()
    {
        cameraAnimator.SwitchView(4);
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        cameraAnimator.SwitchView(0);
        videoPlayer.Stop();
    }
}
