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
    public GameObject monsterPrefab;
    public bool monsterReady;
    public enum MonsterType { Werewolf, Banshee, Vampire, Poltergeist, WomanInWhite, Chupacabra, Mothman, Skinwalker, Doppelganger, JerseyDevil, Demon, HopskinvilleGoblin, Slenderman, BlackShuck, TheHook }
    public MonsterType type;

    [Header("Evidence")]
    [HideInInspector] public StoryEvidence storyEvidence;
    [HideInInspector] public PoliceReportEvidence policeReportEvidence;
    [HideInInspector] public VideoEvidence videoEvidence;
    [HideInInspector] public AudioEvidence audioEvidence;
    [HideInInspector] public ImageEvidence imageEvidence;

    public List<EvidenceData> storyList;
    public List<EvidenceData> imageList;
    public List<EvidenceData> audioList;

    [Header("Depracated Evidence")]
    public List<EvidenceData> policeReportList;
    public List<EvidenceData> videoList;
    
    

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
    public EvidenceData coldTemperatures = new EvidenceData { evidenceName = "Cold Temperatures", evidenceDescription = "Cold Temperatures Reported During Incidents", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData scratching = new EvidenceData { evidenceName = "Scratching", evidenceDescription = "Scrathcing Sounds Reported", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData humanoid = new EvidenceData { evidenceName = "Humanoids", evidenceDescription = "Humanoid Creature Spotted During Incidents", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData flyingCreature = new EvidenceData { evidenceName = "Flying", evidenceDescription = "A Flying Creature Is Seen During Incident", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData disappearingFigure = new EvidenceData { evidenceName = "Disappering Figure", evidenceDescription = "A Figure Is seen vanishing into thin air", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData beastLike = new EvidenceData { evidenceName = "Beast Like", evidenceDescription = "A Beast Like Creature often described as Furry or Animal Like", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData missingPerson = new EvidenceData { evidenceName = "Missing Person", evidenceDescription = "A person is reported missing during an Incident", evidenceType = EvidenceData.EvidenceType.Story };
    public EvidenceData memoryGaps = new EvidenceData { evidenceName = "Memory Gaps", evidenceDescription = "Gaps in memories proceeding or during anomoly incidents", evidenceType = EvidenceData.EvidenceType.Story };
}

[System.Serializable]
public class ImageEvidence
{
    [Header("Image Evidence")]
    public EvidenceData negativeSpace = new EvidenceData { evidenceName = "Negative Spacing", evidenceDescription = "When inverting images figures that weren't previously there show up.", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData thermalSignature = new EvidenceData { evidenceName = "Thermal Signatures", evidenceDescription = "When checking thermal Levels unsual Signatures appear.", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData clawMarks = new EvidenceData { evidenceName = "Claw Marks", evidenceDescription = "Visible Claw Marks on the sides of trees or buildings", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData starSigil = new EvidenceData { evidenceName = "Star Sigil", evidenceDescription = "A sigil in the form of a 7 pointed star is seen somewhere in the image.", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData crossArrowSigil = new EvidenceData { evidenceName = "Cross Arrow Sigil", evidenceDescription = "A sigil in the form of a Cross with an arrow tip is seen somewhere in the image.", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData obscuredFaces = new EvidenceData { evidenceName = "Obscured Faces", evidenceDescription = "An obscured face is seen somewhere in the image", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData possessedPerson = new EvidenceData { evidenceName = "Possessed Person", evidenceDescription = "Through photo editing you can see residue of human possession", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData shadowFigures = new EvidenceData { evidenceName = "Shadow Figure", evidenceDescription = "A shadowy figure is seen in the image often obscured.", evidenceType = EvidenceData.EvidenceType.Image };
    public EvidenceData bloodPresent = new EvidenceData { evidenceName = "Blood Present", evidenceDescription = "Blood is clearly seen visibly or through UV photography", evidenceType = EvidenceData.EvidenceType.Image };


}

[System.Serializable]
public class AudioEvidence
{
    [Header("Audio Evidence")]
    public EvidenceData whispering = new EvidenceData { evidenceName = "Whispering", evidenceDescription = "Whispering can clearly be heard after fixing audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData crying = new EvidenceData { evidenceName = "Crying", evidenceDescription = "Crying can clearly be heard after fixing audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData growls = new EvidenceData { evidenceName = "Growling", evidenceDescription = "Growling can clearly be heard after fixing audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData mimicry = new EvidenceData { evidenceName = "Mimicry", evidenceDescription = "Voices can be heard mimicking voices the player has heard", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData scratching = new EvidenceData { evidenceName = "Scratching", evidenceDescription = "Scratching can clearly be heard after fixing audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData spectrogramSymbols = new EvidenceData { evidenceName = "Symbols", evidenceDescription = "Symbols can be seen in spectrographic transcript of audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData missingAudio = new EvidenceData { evidenceName = "Missing Audio", evidenceDescription = "Chunks of are clearly missing from the audio", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData spectrogramWords = new EvidenceData { evidenceName = "Spectrogram Words", evidenceDescription = "Words are Embedded in to audio source", evidenceType = EvidenceData.EvidenceType.Audio };
    public EvidenceData reverseSpeech = new EvidenceData { evidenceName = "Reverse Speech", evidenceDescription = "After reversing audio words become legible", evidenceType = EvidenceData.EvidenceType.Audio };
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
    public EvidenceData anytimeReports = new EvidenceData { evidenceName = "Appears Anytime", evidenceDescription = "", evidenceType = EvidenceData.EvidenceType.PoliceReport };
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
    public EvidenceData disappearingFigure = new EvidenceData { evidenceName = "Disappering Figure", evidenceDescription = "A Figure Is seen vanishing into thin air", evidenceType = EvidenceData.EvidenceType.Video };
    public EvidenceData flying = new EvidenceData { evidenceName = "Flying Figure", evidenceDescription = "A figure is seen flying in the air", evidenceType = EvidenceData.EvidenceType.Video };
}

[System.Serializable]
public class EvidenceData
{
    //{ get; set; }
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