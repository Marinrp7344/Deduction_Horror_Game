using UnityEngine;

public class AudioHallucination : Hallucination
{
    public AudioSource source;
    public bool startedAudio;

    public override void Update()
    {
        base.Update();
        if(active && !startedAudio)
        {
            source.Play();
            startedAudio = true;
        }


        if(startedAudio)
        {
            if(!source.isPlaying)
            {
                startedAudio = false;
                ClearHallucination();
            }
        }
    }
}
