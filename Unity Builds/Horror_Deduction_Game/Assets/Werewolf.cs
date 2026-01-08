using UnityEngine;

public class Werewolf : Enemy
{
    [Header("Werewolf Information")]
    [SerializeField] private Animator werewolfAnimator;
    
    public float setState = -1;
    public int currentState = 0;

    public bool isLunging;
    public GameObject werewolf;
    public Transform playerTransform;

    public float speed;

    //Update is called once per frame
    void Update()
    {
        if(isLunging)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    public override void ChangeState()
    {
        if (isActive)
        {
            if (!isLunging)
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
    }

    public override void MakeEnemyVisible()
    {
        werewolf.SetActive(true);
        werewolfAnimator.enabled = true;
    }

    private void SwitchState()
    {
        int switchStateChance = Random.Range(0, aggression);
        Debug.Log("SwitchState: " + switchStateChance);
        if(switchStateChance == 0)
        {
            setState = setState * -1;
            currentState += 1;
            werewolfAnimator.SetFloat("State", setState);
        }

        if(currentState == 3)
        {
            isLunging = true;
        }
    }

    public override void HitBullet()
    {
        ResetEnemy();
    }

    public override void ResetEnemy()
    {
        setState = -1;
        currentState = 0;
        werewolf.SetActive(false);
        werewolfAnimator.enabled = false;
        isLunging = false;
        isVisible = false;
    }
}
