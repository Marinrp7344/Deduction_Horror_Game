using UnityEngine;
using UnityEngine.Video;

public class Video_Evidence : MonoBehaviour
{
    [SerializeField] public VideoClip clip;
    [SerializeField] public VideoPlayer videoPlayer;
    [SerializeField] public Evidence_Data evidenceSO;
    [SerializeField] public Camera_Animator cameraAnimator;
    

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
