using System.Collections;
using UnityEngine;

public class StandingHallucination : Hallucination
{
    public float deactivateDelay;

    public override void ActivatePhysicalHallucinationProperties()
    {
        StartCoroutine(Deactivate());
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(deactivateDelay);
        ClearHallucination();
    }

}
