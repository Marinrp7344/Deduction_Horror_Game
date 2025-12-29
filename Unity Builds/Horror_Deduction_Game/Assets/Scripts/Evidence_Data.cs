using UnityEngine;

[CreateAssetMenu(fileName = "Evidence_Data", menuName = "Scriptable Objects/Evidence_Data")]
public class Evidence_Data : Monster_Data
{
    
    public enum Evidence { Story, PoliceReport, Video, Audio, Image }
    [Header("Evidence Type")]
    public Evidence evidenceType;   
}
