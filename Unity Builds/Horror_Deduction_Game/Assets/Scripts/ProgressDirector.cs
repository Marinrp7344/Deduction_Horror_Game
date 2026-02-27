using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ProgressDirector : MonoBehaviour
{
    public static ProgressDirector Instance;
    public ProgressManager progressManager;
    public AudioSource audioPlayer;

    public void Start()
    {
        Instance = this;
        AcheivedStep("Started Game");
        StartCoroutine(TriggerAudioClip());

    }

    public IEnumerator TriggerAudioClip()
    {
        yield return new WaitForSeconds(15);
        StartAudioClip();
    }

    public void StartAudioClip()
    {
        ProgressStep currentStep = FindMostImportantAudioClip();

        if(currentStep != null)
        {
            audioPlayer.clip = currentStep.associatedAudio;
            audioPlayer.Play();
        }
                                
    }

    public ProgressStep FindMostImportantAudioClip()
    {
        if (progressManager.nextAudioClip == null)
            return null;

        int highestPriority = -1;
        ProgressStep mostImportantStep = null;

        foreach(ProgressStep clip in progressManager.nextAudioClip)
        {
            if(clip.priorityLevel > highestPriority)
            {
                highestPriority = clip.priorityLevel;
                mostImportantStep = clip;
            }
        }

        progressManager.nextAudioClip.Remove(mostImportantStep);

        return mostImportantStep;
    }

    public void AdvancedProgress(ProgressStep step)
    {

        if(step.achieved)
        {
            if(step.repeatable)
            {
                progressManager.nextAudioClip.Add(step);
            }
        }
        else
        {
            step.achieved = true;
            progressManager.nextAudioClip.Add(step);
        }
    }

    public void AcheivedStep(string stepName)
    {
        ProgressStep step = progressManager.progessList.Find(s => s.name == stepName);
        if(step != null)
        {
            AdvancedProgress(step);
        }
    }


}
