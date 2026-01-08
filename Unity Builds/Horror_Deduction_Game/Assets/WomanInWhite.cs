using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WomanInWhite : Enemy
{
    [Header("Woman In White Information")]
    public bool playerBeganLooking;
    public int secondsToLookAway;
    public GameObject womanInWhite;
 
    private void Update()
    {
        if (isVisible && !playerBeganLooking)
        {
            TrySeeIfPlayerLooking();
        }

    }
    public override void ChangeState()
    {
        if (isActive)
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
    }

    public override void ResetEnemy()
    {
        playerBeganLooking = false;
        isVisible = false;
        womanInWhite.SetActive(false);
    }


}
