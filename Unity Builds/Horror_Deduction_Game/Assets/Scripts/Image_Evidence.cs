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
    public ImageClassifier currentImage;
    public List<ImageClassifier> currentImages;
    public int currentImageIndex;
    public Camera_Animator player;

    private readonly System.Random rand = new System.Random();

    private void Start()
    {
        currentImageIndex = 0;
        currentImages.Add(evidenceSO.image.image1);
        currentImages.Add(evidenceSO.image.image2);
        currentImages.Add(evidenceSO.image.image3);

        currentImages = GenerateRandomImageLoop(currentImages);

        currentImage = currentImages[0];
        ChangeImage(0);
    }

    public void ActivateImage()
    {
        player.viewsButtons.DisableButtons();
        imageUI.SetActive(true);
        ResetValues();
    }

    public void DeactivateImage()
    {
        player.viewsButtons.ChangeButtons(0);
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
            currentImageIndex = 2;
        }
        else if(currentImageIndex > 2)
        {
            currentImageIndex = 0;
        }

        ChangeTextures(currentImageIndex);

      /*
      switch(currentImageIndex)
      {  
          case 0:
              mainImage.texture = currentImages.image1[0];
              thermalImage.texture = currentImages.image1[1];
              invertImage.texture = currentImages.image1[2];
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
        */
    }

    public void ChangeTextures(int index)
    {
        if (currentImages[index].chosen)
        {
            mainImage.texture = evidenceSO.image.chosenImage.image;
        }
        
    }

    public List<ImageClassifier> GenerateRandomImageLoop(List<ImageClassifier> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            ImageClassifier value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }
}



