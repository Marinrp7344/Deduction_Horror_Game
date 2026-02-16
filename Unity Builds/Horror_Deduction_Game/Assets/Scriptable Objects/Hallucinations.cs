using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hallucinations", menuName = "Scriptable Objects/Hallucinations")]
public class Hallucinations : ScriptableObject
{
    public List<HallucinationElement> hallucinationElements;
}

[System.Serializable]
public class HallucinationElement
{
    public string name;
    public GameObject hallucinationPrefab;
    public AudioClip halucinationAudio;
    public int rarity;
    public bool activated;
    public bool repeatable;
}
