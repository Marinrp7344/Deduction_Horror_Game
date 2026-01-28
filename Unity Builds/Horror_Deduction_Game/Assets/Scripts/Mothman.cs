using UnityEngine;
using System.Collections;
public class Mothman : Enemy
{
    [Header("Mothman Information")]
    public float flyAwaySpeed;
    public bool flyAway;
    public GameObject mothman;
    public Vector3 targetPosition;
    public float position;
    public float flyAwayOffset;
    public bool beganFlyingAway;
    public AudioSource arriveSound;
    public AudioSource leavingSound;

    private void Update()
    {
        if(isVisible && !beganFlyingAway)
        {
            TryFlyAway();
        }

        if (flyAway == true)
        {
            FlyAway();
        }

        if(!isVisible)
        {
            flyAway = false;
        }
    }


    public override void ChangeState()
    {
        if (isActive)
        {
            if (!isVisible && view != player.GetCurrentView())
            {
                CheckVisibility();
            }
        }
    }

    private void FlyAway()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, flyAwaySpeed * Time.deltaTime);
        float positionDistance = Mathf.Abs(Vector3.Distance(targetPosition, transform.position));

        position = positionDistance;

        if(positionDistance < .1f)
        {
            flyAway = false;
            SpawnNewMonster();
        }
    }

    private void SpawnNewMonster()
    {
        Debug.Log("Spawned New Creature");
        DeactivateEnemy();
    }

    public void TryFlyAway()
    {
        if(player.GetCurrentView() == 2)
        {
            beganFlyingAway = true;
            StartCoroutine(FlyAwayOffset());
        }
    }

    public IEnumerator FlyAwayOffset()
    {
        yield return new WaitForSeconds(flyAwayOffset);
        flyAway = true;
        leavingSound.Play();
    }

    public override void MakeEnemyVisible()
    {
        mothman.SetActive(true);
        arriveSound.Play();
    }

    public override void ResetEnemy()
    {
        mothman.SetActive(false);
        isVisible = false;
        beganFlyingAway = false;
        flyAway = false;
        transform.position = spawnPosition;
    }

    public override void HitBullet()
    {
        ResetEnemy();
    }

}
