using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "Evidence_Data", menuName = "Scriptable Objects/Evidence_Data")]
public class Evidence_Data : Monster_Data
{
    
    public enum Evidence { Story, PoliceReport, Video, Audio, Image }
    [Header("Evidence Type")]
    public Evidence evidenceType;
    public GameObject evidencePrefab;

    [Header("Story Evidence Details")]
    public string storyName;
    public string storyDate;
    public string storyDescription;
    public string storyComments;

    [Header("Police Report Evidence Details")]
    public string policeReportTitle;
    public string policeReportDescription;

    [Header("Video Evidence Details")]
    public VideoClip videoClip;

    [Header("Audio Evidence Details")]
    public AudioClip audioClip;
    public AudioClip mimicClip;

    [Header("Image Details")]
    public ImageInfo image;

}

[System.Serializable]
public class ImageInfo
{
    public ImageClassifier image1;
    public ImageClassifier image2;
    public ImageClassifier image3;

    public ImageDataPoint chosenImage;
}

