using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[CreateAssetMenu(fileName = "ImageClassifier", menuName = "Scriptable Objects/ImageClassifier")]
public class ImageClassifier : ScriptableObject
{
    public bool isEvidence;
    public bool isFiller;
    public bool imageUsed;
    public bool chosen;
    [HideInInspector] public ImageData imageData;
    
    [Header("Standard Traits")]
    public ImageDataPoint mainImage = new ImageDataPoint { evidenceName = "Main Image", active = true };
    public ImageDataPoint gradientImage = new ImageDataPoint { evidenceName = "Gradient Image", active = true };
    public ImageDataPoint inverseImage = new ImageDataPoint { evidenceName = "Inverse Image", active = true };

    [Header("Evidence Traits")]
    public List<ImageDataPoint> imageDataPoints;

    private void OnEnable()
    {
        imageDataPoints = ImageListBuilder.Build(imageData);
    }

}

[System.Serializable]
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

[System.Serializable]
public class ImageDataPoint
{
    public string evidenceName;
    public bool active;
    public Texture image;
}
public static class ImageListBuilder
{
    public static List<ImageDataPoint> Build(object evidenceObject)
    {
        var list = new List<ImageDataPoint>();

        if (evidenceObject == null)
            return list;

        var fields = evidenceObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (field.FieldType != typeof(ImageDataPoint))
                continue;

            var evidenceData = field.GetValue(evidenceObject) as ImageDataPoint;

            if (evidenceData != null)
                list.Add(evidenceData);
        }

        return list;
    }
}