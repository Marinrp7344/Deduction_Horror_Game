using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Audio_Random_Outside : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private int noiseChance;
    [SerializeField] private AudioSource noiseSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(OutsideNoise());
    }

    private IEnumerator OutsideNoise()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (!noiseSource.isPlaying)
            {
                int noiseProbability = Random.Range(0, noiseChance);

                if (noiseProbability == 1)
                {
                    int chosenClip = Random.Range(0, clips.Count);
                    noiseSource.clip = clips[chosenClip];
                    noiseSource.Play();
                }
            }
        }

    }
}
