using System.Collections.Generic;
using UnityEngine;

public class HopkinsvilleGoblin : Enemy
{
    public int activeGoblins;
    public int maxGoblins;
    public List<HopkinsvilleGoblin_Child> goblins;

    public override void ChangeState()
    {
        Debug.Log("Goblin Attempt Spawn");
        if(isActive)
        {
            SpawnGoblinChance();
        }
    }

    public void SpawnGoblinChance()
    {
        int switchStateChance = Random.Range(0, aggression);
        Debug.Log("SwitchState: " + switchStateChance);
        if (switchStateChance == 0)
        {
            SpawnGoblin();
        }

        if(activeGoblins >= maxGoblins)
        {
            EnemyAttack();
        }
    }

    public void SpawnGoblin()
    {
        bool goblinFound = false;

        while(!goblinFound)
        {
            int randomGoblin = Random.Range(0, goblins.Count);
            if (goblins[randomGoblin].goblinActive == false && goblins[randomGoblin].occupiedView != player.GetCurrentView())
            {
                goblinFound = true;
                goblins[randomGoblin].ActivateGoblin();
                activeGoblins += 1;
                Debug.Log("Goblin Spawned");

            }
        }
    }

    public void GoblinHit(HopkinsvilleGoblin_Child goblinHit)
    {
        foreach(HopkinsvilleGoblin_Child goblin in goblins)
        {
            if(goblinHit.goblinID == goblin.goblinID)
            {
                goblinHit.DeactivateGoblin();
                activeGoblins -= 1;
            }
        }
    }
}
