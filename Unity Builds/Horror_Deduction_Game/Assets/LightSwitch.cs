using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool lightOn;
    public float onAngle;
    public float offAngle;
    public Light lightSwitch;
    public float onIntensity;
    public float offIntensity;
    public Transform switchTransform;
    public AudioSource source;

    public bool lightSwitchToggleable;
    public float maxTime;
    public float currentTime;
    public bool lightBroken;

    public void Update()
    {
        if(!lightBroken && lightSwitchToggleable)
        {
            LightIncrementer();
        }
    }

    public void LightIncrementer()
    {
        if (lightOn)
        {
            DecrementLightTimer();
        }
        else
        {
            IncrementLightTimer();
        }
        CheckLightTimer();
        CheckLightBroken();
    }

    public void CheckLightBroken()
    {
        if (currentTime <= 0)
        {
            lightBroken = true;
        }
    }

    public void CheckLightTimer()
    {
        if(currentTime > maxTime)
        {
            currentTime = maxTime;
        }
        else if(currentTime < 0)
        {
            currentTime = 0;
        }
    }

    public void DecrementLightTimer()
    {
        currentTime -= Time.deltaTime;
    }

    public void IncrementLightTimer()
    {
        currentTime += Time.deltaTime;
    }

    public void LightSwitchInteraction()
    {
        if (lightSwitchToggleable && !lightBroken)
        {
            switch (lightOn)
            {
                case true:
                    SwitchLightOff();
                    break;
                case false:
                    SwitchLightOn();
                    break;
            }
        }

        source.Play();
    }

    public void SwitchLightOff()
    {
        lightOn = false;
        switchTransform.localRotation = Quaternion.Euler(offAngle, 0, 0);
        lightSwitch.intensity = offIntensity;
    }

    public void SwitchLightOn()
    {
        lightOn = true;
        switchTransform.localRotation = Quaternion.Euler(onAngle, 0, 0);
        lightSwitch.intensity = onIntensity;
    }
}
