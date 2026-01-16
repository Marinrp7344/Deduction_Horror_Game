using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Image_Evidence : MonoBehaviour
{
    public RawImage mainImage;
    public RawImage thermalImage;
    public RawImage invertImage;

    public Material imageMaterial;

    public Slider exposure;
    public Slider saturation;
    public Slider contrast;
    public Slider thermal;
    public Toggle invert;

    public GameObject imageUI;

    public Evidence_Data evidenceSO;
    public ImageInfo currentImage;
    public int currentImageIndex;

    private void Awake()
    {
        currentImageIndex = 0;
        currentImage = evidenceSO.image;
        ChangeImage(0);
    }

    public void ActivateImage()
    {
        imageUI.SetActive(true);
        ResetValues();
    }

    public void DeactivateImage()
    {
        imageUI.SetActive(false);
        ResetValues();
    }

    public void InvertImage(bool isOn)
    {
        if (!isOn)
        {
            invertImage.gameObject.SetActive(false);
        }
        else
        {
            invertImage.gameObject.SetActive(true);
        }
    }

    public void SetExposure(float value)
    {
        imageMaterial.SetFloat("_Exposure", value);
    }

    public void SetSaturation(float value)
    {
        imageMaterial.SetFloat("_Saturation", value);
    }

    public void SetContrast(float value)
    {
        imageMaterial.SetFloat("_Contrast", value);
    }

    public void SetThermal(float value)
    {
        Color texColor = thermalImage.color;

        Color newColor = new Color(texColor.r, texColor.g, texColor.b, value);

        thermalImage.color = newColor;
    }

    public void ResetValues()
    {
        imageMaterial.SetFloat("_Exposure", 1f);
        imageMaterial.SetFloat("_Saturation", 1f);
        imageMaterial.SetFloat("_Contrast", 1f);
        invert.isOn = false;

        exposure.value = 1f;
        saturation.value = 1f;
        contrast.value = 1f;
        thermal.value = 0f;
    }

    public void ChangeImage(int changeValue)
    {
        currentImageIndex += changeValue;

        if(currentImageIndex < 0)
        {
            currentImageIndex = 3;
        }
        else if(currentImageIndex > 3)
        {
            currentImageIndex = 0;
        }

        switch(currentImageIndex)
        {
            case 0:
                mainImage.texture = currentImage.image1[0];
                thermalImage.texture = currentImage.image1[1];
                invertImage.texture = currentImage.image1[2];
                break;
            case 1:
                mainImage.texture = currentImage.image2[0];
                thermalImage.texture = currentImage.image2[1];
                invertImage.texture = currentImage.image2[2];
                break;
            case 2:
                mainImage.texture = currentImage.image3[0];
                thermalImage.texture = currentImage.image3[1];
                invertImage.texture = currentImage.image3[2];
                break;
            case 3:
                mainImage.texture = currentImage.image4[0];
                thermalImage.texture = currentImage.image4[1];
                invertImage.texture = currentImage.image4[2];
                break;

        }
    }

}


