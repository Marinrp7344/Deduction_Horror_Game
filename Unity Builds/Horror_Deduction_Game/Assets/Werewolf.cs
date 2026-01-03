using UnityEngine;

public class Werewolf : MonoBehaviour
{
    [SerializeField] private Animator werewolfAnimator;

    public float setState;
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        werewolfAnimator.SetFloat("State", setState);
    }

    private void ChangeWerewolfState(Enemy_Director enemy)
    {

    }
}
