using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WomanInWhite : Enemy
{
    [Header("Woman In White Information")]
    public bool playerBeganLooking;
    public int secondsToLookAway;
    public GameObject womanInWhite;
    public AudioSource arriveAudio;
 
    private void Update()
    {
        if (isVisible && !playerBeganLooking)
        {
            TrySeeIfPlayerLooking();
        }

    }
    public override void ChangeState()
    {
        if (isActive && player.GetCurrentView() == 0)
        {
            if (!isVisible)
            {
                CheckVisibility();
            }
        }
    }

    public void TrySeeIfPlayerLooking()
    {
        if(player.GetCurrentView() == 1)
        {
            playerBeganLooking = true;
            StartCoroutine(ChanceToLookAway());
        }
    }

    public IEnumerator ChanceToLookAway()
    {
        yield return new WaitForSeconds(secondsToLookAway);
        if (player.GetCurrentView() != 0)
        {
            EnemyAttack();
        }
        else
        {
            ResetEnemy();
        }
    }

    public override void MakeEnemyVisible()
    {
        womanInWhite.SetActive(true);
        arriveAudio.Play();
    }

    public override void ResetEnemy()
    {
        playerBeganLooking = false;
        isVisible = false;
        womanInWhite.SetActive(false);
    }

    public override void EnemyAttack()
    {
        ProgressDirector.Instance.AcheivedStep("Died To Woman In White");
        base.EnemyAttack();
    }


}
