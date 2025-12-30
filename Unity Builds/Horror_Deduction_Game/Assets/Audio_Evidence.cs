using UnityEngine;

public class Audio_Evidence : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    
    public void PlayAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
