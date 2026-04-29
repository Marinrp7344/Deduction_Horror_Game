using System.Collections.Generic;
using UnityEngine;

public class Skinwalker : Enemy
{
    public bool exposed;
    public int position;
    public GameObject normalBody;
    public GameObject decomposedBody;
    public int changeChances;
    public int stateChangeAmounts;
    public List<Vector3> skinwalkerLocations;
    public List<Vector3> skinwalkerRotations;
    public Transform skinwalkerTransform;
    public AudioSource exposingSound;
    
    public override void ChangeState()
    {
        if (isActive && view != player.GetCurrentView())
        {
            if (!isVisible)
            {
                CheckVisibility();
            }
            else if(isVisible && !exposed && player.GetCurrentView() != view)
            {
                AttemptToChangeAppearnce();
            }
            else if(isVisible && exposed && player.GetCurrentView() != view)
            {
                AttemptToAdvance();
            }
        }
    }

    public void AttemptToChangeAppearnce()
    {
        int randomAppearanceChance = Random.Range(0, changeChances);

        if(randomAppearanceChance == 0)
        {
            normalBody.SetActive(false);
            decomposedBody.SetActive(true);
            exposed = true;
            stateChangeAmounts = 0;
        }
    }

    public void AttemptToAdvance()
    {
        stateChangeAmounts += 1;

        if(stateChangeAmounts >= 3 && position < 2)
        {
            skinwalkerTransform.position = skinwalkerLocations[position];
            skinwalkerTransform.rotation = Quaternion.Euler(skinwalkerRotations[position].x, skinwalkerRotations[position].y, skinwalkerRotations[position].z);
            position += 1;
            normalBody.SetActive(true);
            decomposedBody.SetActive(false);
        }
        else if(stateChangeAmounts >= 3 && position >= 2)
        {
            EnemyAttack();
        }

    }

    public override void HitBullet()
    {
        if(exposed)
        {
            ResetEnemy();
        }
    }

    public override void ResetEnemy()
    {
        exposed = false;
        normalBody.SetActive(false);
        decomposedBody.SetActive(false);
        position = 0;
        stateChangeAmounts = 0;
        skinwalkerTransform.position = spawnPosition;
    }

    
}
