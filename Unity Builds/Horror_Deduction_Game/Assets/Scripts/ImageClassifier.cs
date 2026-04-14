using UnityEngine;
using System.Collections.Generic;
using System.Collections;
[CreateAssetMenu(fileName = "ImageClassifier", menuName = "Scriptable Objects/ImageClassifier")]
public class ImageClassifier : ScriptableObject
{
    public bool isEvidence;
    public bool isFiller;
    public bool imageUsed;

}

public class ImageData
{
    public ImageDataPoint negativeSpace = new ImageDataPoint { evidenceName = "Negative Spacing"};
    public ImageDataPoint thermalSignature = new ImageDataPoint { evidenceName = "Thermal Signatures"};
    public ImageDataPoint clawMarks = new ImageDataPoint {evidenceName = "Claw Marks"   };
    public ImageDataPoint starSigil = new ImageDataPoint {evidenceName = "Star Sigil" };
    public ImageDataPoint crossArrowSigil = new ImageDataPoint {evidenceName = "Cross Arrow Sigil" };
    public ImageDataPoint obscuredFaces = new ImageDataPoint {evidenceName = "Obscured Faces" };
    public ImageDataPoint possessedPerson = new ImageDataPoint {evidenceName = "Possessed Person" };
    public ImageDataPoint shadowFigures = new ImageDataPoint {evidenceName = "Shadow Figure" };
    public ImageDataPoint bloodPresent = new ImageDataPoint {evidenceName = "Blood Present" };
}

public class ImageDataPoint
{
    public string evidenceName;
    public bool active;
    public Sprite image;
}
