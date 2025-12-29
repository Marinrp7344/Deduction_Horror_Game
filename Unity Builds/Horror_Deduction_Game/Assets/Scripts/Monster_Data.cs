using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

[CreateAssetMenu(fileName = "Monster_Data", menuName = "Scriptable Objects/Monster_Data")]
public class Monster_Data : ScriptableObject
{
    [Header("Monster Information")]
    public string monsterName;
    public string monsterDescription;

    [Header("Evidence")]
    [HideInInspector] public StoryEvidence storyEvidence;
    [HideInInspector] public PoliceReportEvidence policeReportEvidence;
    [HideInInspector] public VideoEvidence videoEvidence;
    [HideInInspector] public AudioEvidence audioEvidence;
    [HideInInspector] public ImageEvidence imageEvidence;

    public List<EvidenceData> storyList;
    public List<EvidenceData> policeReportList;
    public List<EvidenceData> videoList;
    public List<EvidenceData> audioList;
    public List<EvidenceData> imageList;

    private void OnEnable()
    {
        storyList = EvidenceListBuilder.Build(storyEvidence);
        policeReportList = EvidenceListBuilder.Build(policeReportEvidence);
        videoList = EvidenceListBuilder.Build(videoEvidence);
        audioList = EvidenceListBuilder.Build(audioEvidence);
        imageList = EvidenceListBuilder.Build(imageEvidence);
    }
}

[System.Serializable]
public class StoryEvidence
{
    [Header("Story Evidence")]
    public EvidenceData deadPets = new EvidenceData { evidenceName = "Dead Pets", evidenceDescription = "Reports of Dead Pets from Civilians", evidenceType = EvidenceData.EvidenceType.Story};
    public EvidenceData lateNightReports = new EvidenceData { evidenceName = "Late Night Reports", evidenceDescription = "Civilians Report Incidents Past 12am", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData coldTemperatures = new EvidenceData { evidenceName = "Cold Temperatures", evidenceDescription = "Cold Temperatures Reported During Incidents", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData scratching = new EvidenceData { evidenceName = "Scratching", evidenceDescription = "Scrathcing Sounds Reported", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData humanoid = new EvidenceData { evidenceName = "Humanoids", evidenceDescription = "Humanoid Creature Spotted During Incidents", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData flyingCreature = new EvidenceData { evidenceName = "Flying", evidenceDescription = "A Flying Creature Is Seen During Incident", evidenceType = EvidenceData.EvidenceType.Story };
}

[System.Serializable]
public class PoliceReportEvidence
{
    [Header("Police Evidence")]
    public EvidenceData propertyDamage = new EvidenceData { evidenceName = "Property Damage", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData humanoidSuspect = new EvidenceData { evidenceName = "Humanoid Suspect", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData missingPeople = new EvidenceData { evidenceName = "Missing People", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData bloodDrainedVictims = new EvidenceData { evidenceName = "Blood Drained Humans or Animals", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData suddenDeaths = new EvidenceData { evidenceName = "Sudden Deaths", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData eveningReports = new EvidenceData { evidenceName = "Evening Reports", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
    public EvidenceData animalAttacks = new EvidenceData { evidenceName = "Animal Attacks", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
}

[System.Serializable]
public class VideoEvidence
{
    [Header("Video Evidence")]
    public EvidenceData invisibleAssailant = new EvidenceData { evidenceName = "Invisible Assailant", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData floatingObjects = new EvidenceData { evidenceName = "Floating Object", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData cameraDistortion = new EvidenceData { evidenceName = "Camera Distortion", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData coldSpots = new EvidenceData { evidenceName = "Cold Spots", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData bulkyCreature = new EvidenceData { evidenceName = "Bulky Creature", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData animalLike = new EvidenceData { evidenceName = "Animal Like", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Video };
}

[System.Serializable]
public class AudioEvidence
{
    [Header("Audio Evidence")]
    public EvidenceData audioDistortions = new EvidenceData { evidenceName = "Audio Distortions", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData whispering = new EvidenceData { evidenceName = "Whispering", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData wailing = new EvidenceData { evidenceName = "Wailing", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData howls = new EvidenceData { evidenceName = "Howling", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData growls = new EvidenceData { evidenceName = "Growling", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Audio };
}

[System.Serializable]
public class ImageEvidence
{
    [Header("Image Evidence")]
    public EvidenceData invertedImaging = new EvidenceData { evidenceName = "Inverse Imaging", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData negativeSpace = new EvidenceData { evidenceName = "Negative Spacing", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData heatMap = new EvidenceData { evidenceName = "Heat Maps", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData clawMarks = new EvidenceData { evidenceName = "Claw Marks", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.Image };

}

[System.Serializable]
public class EvidenceData
{
    public string evidenceName;
    public string evidenceDescription;
    public bool evidenceRelevant;
    public enum EvidenceType { Story, PoliceReport, Video, Audio, Image }
    public EvidenceType evidenceType;
}

public static class EvidenceListBuilder
{
    public static List<EvidenceData> Build(object evidenceObject)
    {
        var list = new List<EvidenceData>();

        if (evidenceObject == null)
            return list;

        var fields = evidenceObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (field.FieldType != typeof(EvidenceData))
                continue;

            var evidenceData = field.GetValue(evidenceObject) as EvidenceData;

            if (evidenceData != null)
                list.Add(evidenceData);
        }

        return list;
    }
}