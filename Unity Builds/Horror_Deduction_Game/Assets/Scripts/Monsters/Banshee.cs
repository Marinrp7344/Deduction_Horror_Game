using UnityEngine;

public class Banshee : Enemy
{
    [Header("Banshee Information")]
    public GameObject banshee;
    public float maxCharge;
    public float currentCharge;
    public float chargeIncreaseRate;
    public AudioSource arriveAudio;

    public override void ChangeState()
    {
        if (isActive && view != player.GetCurrentView())
        {
            if (!isVisible)
            {
                CheckVisibility();
            }
            else
            {
                CheckIfAddToMultiplier();
            }
        }   
    }
    public void CheckIfAddToMultiplier()
    {
        int switchStateChance = Random.Range(0, aggression);
        Debug.Log("SwitchState: " + switchStateChance);

        if (switchStateChance == 0)
        {
            enemyDirector.bansheeMultiplier += 1;
        }
    }

    public override void MakeEnemyVisible()
    {
        banshee.SetActive(true);
        arriveAudio.Play();
    }


    public override void ResetEnemy()
    {
        isVisible = false;
        banshee.SetActive(false);
        enemyDirector.bansheeMultiplier = 1;
        currentCharge = 0;
    }

    public override void HitCrucifix()
    {
        if(currentCharge > maxCharge)
        {
            ResetEnemy();
        }
        else
        {
            currentCharge += chargeIncreaseRate * Time.deltaTime;
        }
        

    }

}
