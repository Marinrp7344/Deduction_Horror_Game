using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "CompendiumManager", menuName = "Scriptable Objects/CompendiumManager")]
public class CompendiumManager : ScriptableObject
{
    public List<CompendiumElements> elements;
}

[System.Serializable]
public class CompendiumElements
{
    public string elementName;
    public List<string> acceptableSearchTerms;
    public bool discovered;
    public Sprite elementImage;
    public AudioClip elementAudio;
    public enum ElementType { None, Image, Audio}
    public ElementType elementType;
}
