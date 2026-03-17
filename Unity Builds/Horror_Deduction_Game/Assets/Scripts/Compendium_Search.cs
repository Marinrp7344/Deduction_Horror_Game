using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Compendium_Search : MonoBehaviour
{
    [SerializeField] private TMP_InputField search;
    [SerializeField] private CompendiumManager compendiumManager;
    [SerializeField] private Image displayImage;
    [SerializeField] private AudioSource displayAudio;
    [SerializeField] private RectTransform loadingScreen;
    [SerializeField] private float loadSpeed;
    [SerializeField] private GameObject typeSound;
    public void SearchCompendium()
    {
        Debug.Log("Search Term: " + search.text);
        if(compendiumManager.elements !=  null)
        {
            foreach(CompendiumElements element in compendiumManager.elements)
            {
                if(element.acceptableSearchTerms != null)
                {
                    foreach(string searchTerm  in element.acceptableSearchTerms)
                    {
                        Debug.Log("Element Term: " + searchTerm);
                        if (search.text == searchTerm)
                        {
                            Debug.Log("Success Search");
                            DisplayElement(element);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void DisplayElement(CompendiumElements element)
    {
        switch (element.elementType)
        {
            case CompendiumElements.ElementType.Image:
                Debug.Log("Success Image");
                ProcessImageElement(element);
                break;
            case CompendiumElements.ElementType.Audio:
                ProcessAudioElement();
                break;
        }
    }

    public void ProcessImageElement(CompendiumElements element)
    {
        loadingScreen.localScale = new Vector3(1f,1.2f,1f);
        displayImage.enabled = true;
        displayImage.sprite = element.elementImage;
        element.discovered = true;
        StartCoroutine(LoadImage());
    }

    public IEnumerator LoadImage()
    {
        float t = 1.2f;
        Debug.Log("Success Load");
        while (t > 0f) 
        {
            t -= Time.deltaTime * loadSpeed;

            if (t < 0f)
            {
                loadingScreen.localScale = new Vector3(1, 0f, 1);
            }
            else
            {
                loadingScreen.localScale = new Vector3(1, t, 1);
            }
            
            yield return new WaitForSeconds(Random.Range(0.02f, 0.3f));
        }
        loadingScreen.localScale = new Vector3(1, 0f, 1);
    }

    public void ProcessAudioElement()
    {

    }

    public void SpawnTypeSound()
    {
        Instantiate(typeSound, gameObject.transform.position,Quaternion.identity);
    }
}
