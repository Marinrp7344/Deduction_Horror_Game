using UnityEngine;

public class Chupacabra : Enemy
{
    [Header("Chupacabra Information")]
    public GameObject chupacabra;
    public bool moving;
    public Vector3 targetPosition;
    public float moveSpeed;
    public bool reachedPosition;
    public bool enemyAttacked;

    private void Update()
    {
        if (!enemyAttacked)
        {
            if (moving)
            {
                Move();
            }

            if (reachedPosition)
            {
                EnemyAttack();
                enemyAttacked = true;
            }
        }
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        float positionDistance = Mathf.Abs(Vector3.Distance(targetPosition, transform.position));

        if (positionDistance < .1f)
        {
            moving = false;
            reachedPosition = true;
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
            else
            {
                if(!moving && !reachedPosition)
                {
                    moving = true;
                }
            }
        }
    }

    public override void MakeEnemyVisible()
    {
        chupacabra.SetActive(true);
        SpawnSound();
    }

    public override void ResetEnemy()
    {
        isVisible = false;
        chupacabra.SetActive(false);
        moving = false;
        transform.position = spawnPosition;
    }

    public override void HitBullet()
    {
        ResetEnemy();
    }

    public override void SpawnSound()
    {
        startingAudio.Play();
    }
}
