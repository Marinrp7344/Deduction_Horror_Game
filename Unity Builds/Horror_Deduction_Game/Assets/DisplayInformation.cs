using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInformation : MonoBehaviour
{
    public Director director;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void DisplayAllInfo()
    {
        foreach(Monster_Data monster in director.monsters)
        {
            int i = 0;
            foreach(EvidenceData storyData in monster.storyList)
            {
                if(storyData.evidenceRelevant == true)
                {
                    foreach(EvidenceData imageData in monster.imageList)
                    {
                        int j = 0;
                        if(imageData.evidenceRelevant == true)
                        {
                            foreach(EvidenceData audioData in  monster.audioList)
                            {
                                
                                FindRelevantMonsters(storyData, imageData, audioData);
                            }
                        }
                        j++;
                    }
                }
                i++;
            }
        }
    }

    public void FindRelevantMonsters(EvidenceData storyData, EvidenceData imageData, EvidenceData audioData)
    {
        List<Monster_Data> monstersFoundStory = new List<Monster_Data>();
        List<Monster_Data> monstersFoundImage = new List<Monster_Data>();
        List<Monster_Data> monstersFoundAudio = new List<Monster_Data>();

        foreach (Monster_Data monster in director.monsters)
        {
        }
    }
}
