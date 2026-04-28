using UnityEngine;

public class Demon : Enemy
{
    [SerializeField] private bool demonWaiting;

    public float maxCharge;
    public float currentCharge;
    public float chargeIncreaseRate;

    public void Update()
    {
        if (demonWaiting)
        {
            if (lightSwitch.lightOn && player.GetCurrentView() == view)
            {
                EnemyAttack();
            }
        }
    }

    public override void ChangeState()
    {
        if(isActive && player.GetCurrentView() != view)
        {
            if(!isVisible)
            {
                MakeEnemyVisible();
            }
            else
            {
                demonWaiting = true;
            }
        }
    }

    public override void HitCrucifix()
    {
        if (currentCharge > maxCharge)
        {
            ResetEnemy();
        }
        else
        {
            currentCharge += chargeIncreaseRate * Time.deltaTime;
        }


    }


}
