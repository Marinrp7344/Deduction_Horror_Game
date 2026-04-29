using System.Collections.Generic;
using UnityEngine;

public class BlackShuck : Enemy
{
    public bool left;
    public List<Vector3> shuckLocationsLeft;
    public List<Vector3> shuckLocationsRight;
    public List<Vector3> chosenShuckLocations;
    public List<Vector3> shuckRotations;
    public int position;
    public GameObject shuck;
    public Transform shuckTransform;
    public int moveForwardChances;

    public float currentLookTimer;
    public float maxLookTimer;

    public void Update()
    {
        if(isVisible && player.GetCurrentView() == view && lightSwitch.lightOn)
        {
            currentLookTimer -= Time.deltaTime;
            if(currentLookTimer <= 0)
            {
                EnemyAttack();
            }
        }
        else
        {
            currentLookTimer = maxLookTimer;
        }
    }
    public override void ChangeState()
    {
        if (isActive && view != player.GetCurrentView())
        {
            if (!isVisible)
            {
                int randomDirection = Random.Range(0,2);
                if(randomDirection == 0) { left = false; } else { left = true; }
                CheckVisibility();
            }
            else
            {
                AttemptToMoveForward();
            }
        }
    }

    public void AttemptToMoveForward()
    {
        int randomMoveForwardChance = Random.Range(0,moveForwardChances);
        if (randomMoveForwardChance <= 0 && position < 2)
        {
            position += 1;
            shuckTransform.position = chosenShuckLocations[position];
        }
        else if(randomMoveForwardChance <= 0 && position >= 2)
        {
            EnemyAttack();
        }
        
    }

    public override void MakeEnemyVisible()
    {
        base.MakeEnemyVisible();
        shuck.SetActive(true);
        if (left)
        {
            chosenShuckLocations = shuckLocationsLeft;
        }
        else
        {
            chosenShuckLocations = shuckLocationsRight;
        }

        shuckTransform.position = chosenShuckLocations[position];

    }
    public override void HitCrucifix()
    {
        ResetEnemy();
    }

    public override void ResetEnemy()
    {
        position = 0;
        shuck.SetActive(false);
        currentLookTimer = maxLookTimer;
    }
}
