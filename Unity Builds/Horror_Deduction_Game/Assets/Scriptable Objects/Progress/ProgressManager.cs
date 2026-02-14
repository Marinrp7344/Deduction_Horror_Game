using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ProgressManager", menuName = "Scriptable Objects/ProgressManager")]
public class ProgressManager : ScriptableObject
{
    public List<ProgressStep> progessList;
    public List<ProgressStep> nextAudioClip;
}

[System.Serializable]
public class ProgressStep
{
    public string name;
    public bool achieved;
    public bool repeatable;
    public AudioClip associatedAudio;
    public int priorityLevel;
}
