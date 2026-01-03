using UnityEngine;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "Evidence_Data", menuName = "Scriptable Objects/Evidence_Data")]
public class Evidence_Data : Monster_Data
{
    
    public enum Evidence { Story, PoliceReport, Video, Audio, Image }
    [Header("Evidence Type")]
    public Evidence evidenceType;
    public GameObject evidencePrefab;

    [Header("Story Evidence Details")]
    public string storyTitle;
    public string storyDescription;

    [Header("Police Report Evidence Details")]
    public string policeReportTitle;
    public string policeReportDescription;

    [Header("Video Evidence Details")]
    public VideoClip videoClip;

    [Header("Audio Evidence Details")]
    public AudioClip audioClip;

}
