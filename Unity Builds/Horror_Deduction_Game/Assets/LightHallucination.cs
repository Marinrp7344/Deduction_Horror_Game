using System.Collections;
using UnityEngine;

public class LightHallucination : Hallucination
{
    public float flickerTime;
    public float preFlickerTime;
    public AudioSource source;
    public bool started;

    public override void Update()
    {
        base.Update();
        ActivatePhysicalHallucinationProperties();
    }

    public override void ActivatePhysicalHallucinationProperties()
    {
        if (director.lightSwitch.lightOn)
        {
            if (!started)
            {
                started = true;
                StartCoroutine(Deactivate());
            }
        }
    }

    public IEnumerator Deactivate()
    {
        director.lightSwitch.lightSwitchToggleable = false;
        source.Play();
        yield return new WaitForSeconds(preFlickerTime);
        director.lightSwitch.lightSwitch.intensity = 0;
        yield return new WaitForSeconds(flickerTime);
        director.lightSwitch.lightSwitch.intensity = 10;
        yield return new WaitForSeconds(flickerTime);
        director.lightSwitch.lightSwitch.intensity = 0;
        yield return new WaitForSeconds(flickerTime);
        director.lightSwitch.lightSwitch.intensity = 10;
        yield return new WaitForSeconds(flickerTime);
        director.lightSwitch.lightSwitch.intensity = 0;
        yield return new WaitForSeconds(flickerTime);
        director.lightSwitch.lightSwitch.intensity = 10;
        yield return new WaitForSeconds(flickerTime);
        ClearHallucination();
        director.lightSwitch.lightSwitchToggleable = true;
        director.lightSwitch.LightSwitchInteraction();
    }
}
