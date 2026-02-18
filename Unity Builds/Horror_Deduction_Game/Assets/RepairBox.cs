using UnityEngine;
using UnityEngine.UI;

public class RepairBox : MonoBehaviour
{
    public bool repairBoxEnabled;
    public LightSwitch lightSwitch;
    public GameObject text;
    public Image repairImage;
    public Light fuseLight;

    public bool pressingDown;
    public float currentRepairProgress;
    public float neededRepairProgress;

    private void Update()
    {
        if(lightSwitch.lightBroken && !repairBoxEnabled)
        {
            BrokenBox();
        }

        if(pressingDown && repairBoxEnabled)
        {
            RepairingBox();
        }
    }

    public void BrokenBox()
    {
        text.SetActive(true);
        repairImage.gameObject.SetActive(true);
        fuseLight.color = Color.red;
        repairBoxEnabled = true;
    }

    public void FixedBox()
    {
        text.SetActive(false);
        repairImage.gameObject.SetActive(false);
        repairImage.fillAmount = 0f;
        fuseLight.color = Color.white;
        currentRepairProgress = 0;
        lightSwitch.lightBroken = false;
        lightSwitch.currentTime = lightSwitch.maxTime;
        repairBoxEnabled = false;
    }

    public void RepairingBox()
    {
        if(currentRepairProgress <  neededRepairProgress)
        {
            currentRepairProgress += Time.deltaTime;
            repairImage.fillAmount = currentRepairProgress/neededRepairProgress;
        }
        else
        {
            FixedBox();
        }
    }

    public void PressDown()
    {
        pressingDown = true;
    }

    public void LetGo()
    {
        pressingDown = false;
    }
}
