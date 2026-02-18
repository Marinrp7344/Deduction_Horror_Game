using System.Collections;
using UnityEngine;

public class HallucinationDirector : MonoBehaviour
{
    [SerializeField] private Hallucinations hallucinationManager;
    [SerializeField] private bool hallucinationActive;
    [SerializeField] private Hallucination currentHallucination;
    [SerializeField] private int hallucinationLikelihood;
    [SerializeField] private Camera_Animator player;
    [SerializeField] public LightSwitch lightSwitch;
    public void Start()
    {
        StartCoroutine(HallucinationTimer());
    }

    public IEnumerator HallucinationTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!hallucinationActive)
            {
                AttemptHallucination();
            }
        }
    }

    public void ClearHallucination()
    {
        currentHallucination.DestroyHallucination();
        hallucinationActive = false;
    }

    public void AttemptHallucination()
    {
        int hallucinationAttempt = Random.Range(0, hallucinationLikelihood);
        //Debug.Log("Checking Halucination"+ "\nAttempt Value:" + hallucinationAttempt);
        if (hallucinationAttempt == 1)
        {
            //Debug.Log("Choosing Hallucination");
            ChooseHallucination();
        }
    }

    public void ChooseHallucination()
    { 
        foreach(HallucinationElement hallucination in hallucinationManager.hallucinationElements)
        {
            int attemptToActivate = Random.Range(0, hallucination.rarity);
            //Debug.Log("Hallucination Attempt To Activate" + "\nAttempt Value:" + attemptToActivate);

            if (!hallucination.activated || hallucination.repeatable)
            {
                if (attemptToActivate == 1)
                {
                    //Debug.Log("Hallucination Found");
                    ActivateHallucination(hallucination);
                    break;
                }
            }
        }
    }

    public void ActivateHallucination(HallucinationElement hallucinationElement)
    {
        GameObject hallucination = Instantiate(hallucinationElement.hallucinationPrefab, transform.position,Quaternion.identity);
        Hallucination hallucinationScript = hallucination.GetComponent<Hallucination>();
        currentHallucination = hallucinationScript;
        hallucination.transform.position = hallucinationScript.spawnPoint;
        hallucinationScript.hallucinationElement = hallucinationElement;
        hallucinationElement.activated = true;
        hallucinationScript.player = player;
        hallucinationScript.director = this;
        hallucinationActive = true;
    }


}
