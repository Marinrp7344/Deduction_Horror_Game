using System.Collections;
using UnityEngine;

public class Hallucination : MonoBehaviour
{
    public Vector3 spawnPoint;
    public int viewPosition;
    public bool viewed;
    public bool active;
    public bool physicalHallucination;
    public float activateDelay;
    public Camera_Animator player;
    public GameObject body;
    public HallucinationElement hallucinationElement;
    public HallucinationDirector director;

    public void DestroyHallucination()
    {
        Destroy(gameObject);
    }

    public virtual void Update()
    {
        if(!active && !physicalHallucination)
        {
            active = true;
            ActivateAudioHallucinationProperties();
        }

        if (!active && physicalHallucination)
        {
            CheckPlayerInSameView();
        }

        if(active)
        {
            CheckIfViewed();
        }

        if(viewed && physicalHallucination)
        {
            StartCoroutine(HallucinationDelay());
        }

    }

    public void CheckPlayerInSameView()
    {
        if(player.GetCurrentView() != viewPosition)
        {
            active = true;
            body.SetActive(true);
        }
    }

    public void CheckIfViewed()
    {
        if(player.GetCurrentView() == viewPosition)
        {
            viewed = true;
        }
    }

    public IEnumerator HallucinationDelay()
    {
        yield return new WaitForSeconds(activateDelay);
        ActivatePhysicalHallucinationProperties();
    }

    public virtual void ActivatePhysicalHallucinationProperties()
    {

    }

    public virtual void ActivateAudioHallucinationProperties()
    {

    }

    public void ClearHallucination()
    {
        director.ClearHallucination();
    }
}
