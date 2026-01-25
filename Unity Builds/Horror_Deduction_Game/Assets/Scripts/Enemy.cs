using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General Enemy Information")]
    public bool isActive;
    public bool isVisible;
    public Enemy_Director enemyDirector;
    public Monster_Data.MonsterType monsterType;
    public Vector3 spawnPosition;
    public int aggression;
    public int visibleMultiplier;
    public int visibleChanceThreshold;
    public Camera_Animator player;
    public float rotationY;
    public float rotationX;
    public int view;

    public float enemyFrequency;

    public AudioSource startingAudio;


    private void Start()
    {
        transform.rotation = Quaternion.Euler(rotationX, rotationY, transform.rotation.z);
        enemyFrequency = Random.Range(0f, 360f);
    }

    public virtual void ResetEnemy()
    {
        Debug.Log("Enemy Reset");
    }

    public void AddListener()
    {
        enemyDirector.ChangeState.AddListener(ChangeState);
    }

    public virtual void ChangeState()
    {
        Debug.Log("Change State");
    }

    public void CheckVisibility()
    {
        int visibleChance = aggression * visibleMultiplier;
        int likelihoodToBecomeVisible = Random.Range(0, visibleChance);

        Debug.Log("Likliehood: " + likelihoodToBecomeVisible);
        if (likelihoodToBecomeVisible > visibleChanceThreshold)
        {
            isVisible = true;
            MakeEnemyVisible();
        }
    }

    public virtual void MakeEnemyVisible()
    {
        Debug.Log("Enemy Visible");
        enemyFrequency = Random.Range(0f, 360f);
    }

    public void DeactivateEnemy()
    {
        enemyDirector.activeEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public virtual void EnemyAttack()
    {
        Debug.Log("Enemy Attack");
    }

    public virtual void HitBullet()
    {
        
    }

    public virtual void HitCrucifix()
    {

    }

    public virtual void SpawnSound()
    {
 
    }
}
