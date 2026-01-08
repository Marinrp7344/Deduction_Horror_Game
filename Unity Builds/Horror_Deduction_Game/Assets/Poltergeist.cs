using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Poltergeist : Enemy
{
    [Header("Poltergeist Information")]
    public List<Poltergeist_Object> poltergeistObjects;
    public GameObject poltergeist;
    public int currentObject;


    //Update is called once per frame
    void Update()
    {

    }

    public override void ChangeState()
    {
        if (isActive)
        {
            if (!isVisible)
            {
                CheckVisibility();
            }
            else
            {
                SwitchState();
            }
        }
    }

    public override void MakeEnemyVisible()
    {
        poltergeist.SetActive(true);
        poltergeistObjects[currentObject].affectedByPoltergeist = true;
        currentObject += 1;
    }

    private void SwitchState()
    {
        int switchStateChance = Random.Range(0, aggression);
        if (switchStateChance == 0)
        {
            if(currentObject <= 2)
            {
                poltergeistObjects[currentObject].affectedByPoltergeist = true;
                currentObject += 1;
            }
            else
            {
                EnemyAttack();
            }
        }
    }

    public override void ResetEnemy()
    {
        foreach(Poltergeist_Object obj in poltergeistObjects)
        {
            obj.affectedByPoltergeist = false;
        }
        currentObject = 0;
        poltergeist.SetActive(false);
        isVisible = false;
    }
}
