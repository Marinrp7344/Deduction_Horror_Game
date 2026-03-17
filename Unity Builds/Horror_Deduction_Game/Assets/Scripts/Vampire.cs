using UnityEngine;

public class Vampire : Enemy
{
    [Header("Vampire Information")]
    public Vector3 position1;
    public Vector3 position2;
    public Vector3 position3;

    public int currentPosition = 0;
    public bool moving;
    public float moveSpeed;
    public GameObject vampire;
    public AudioSource scurrySound;

    private void Update()
    {
        if(moving)
        {
            switch(currentPosition)
            {
                case 1:
                    Move(position2);
                    break;
                case 2:
                    Move(position3);
                    break;
            }
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, moveSpeed * Time.deltaTime);
        float positionDistance = Mathf.Abs(Vector3.Distance(position, transform.position));

        if (positionDistance < .1f)
        {
            moving = false;
        }
    }

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
                SwitchState();
            }

        }
    }

    public override void MakeEnemyVisible()
    {
        vampire.SetActive(true);
    }

    public override void EnemyAttack()
    {
        ProgressDirector.Instance.AcheivedStep("Died To Vampire");
        base.EnemyAttack();
        
    }

    public void SwitchState()
    {
        int switchStateChance = Random.Range(0, aggression);
        Debug.Log("SwitchState: " + switchStateChance);
        
        if (switchStateChance == 0 && !moving)
        {
            currentPosition += 1;
            if(currentPosition <= 2)
            {
                moving = true;
                scurrySound.Play();
            }
            else
            {
                EnemyAttack();
            }
        }

    }

    public override void ResetEnemy()
    {
        transform.position = position1;
        moving = false;
        isVisible = false;
        vampire.SetActive(false);
        currentPosition = 0;
    }

    public override void HitCrucifix()
    {
        if(currentPosition == 2)
        {
            ResetEnemy();
        }
    }
}
