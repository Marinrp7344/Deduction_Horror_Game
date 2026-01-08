using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Enemy_Director : MonoBehaviour
{
    public UnityEvent ChangeState;
    public Camera_Animator player;
    public List<GameObject> activeEnemies;

    public List<Poltergeist_Object> poltergeistObjects;
    public int bansheeMultiplier;
    private void Start()
    {
        StartCoroutine(ChangeGameState());
    }

    private IEnumerator ChangeGameState()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            ChangeState.Invoke();
            Debug.Log("Test");
        }
    }

    public void SpawnMonster(Monster_Data monster)
    {
        GameObject spawnedMonster = Instantiate(monster.monsterPrefab, transform.position, Quaternion.identity);
        Enemy monsterScript = spawnedMonster.GetComponent<Enemy>();
        spawnedMonster.transform.position = monsterScript.spawnPosition;
        monsterScript.enemyDirector = this;
        monsterScript.player = player;
        monsterScript.AddListener();
        ProcessEnemy(monster, spawnedMonster);
    }

    private void ProcessEnemy(Monster_Data monsterData, GameObject monster)
    {
        switch(monsterData.monsterName)
        {
            case "Werewolf":
                ProcessWerewolf(monsterData, monster);
                break;
            case "Poltergeist":
                ProcessPoltergeist(monsterData, monster);
                break;

        }
    }

    private void ProcessWerewolf(Monster_Data monsterData, GameObject monster)
    {
        Werewolf werewolf = monster.GetComponent<Werewolf>();
        werewolf.playerTransform = player.gameObject.transform;
    }

    private void ProcessBanshee(Monster_Data monsterData, GameObject monster)
    {
        Banshee banshee = monster.GetComponent<Banshee>();
    }

    private void ProcessMothman(Monster_Data monsterData, GameObject monster)
    {
        Mothman mothman = monster.GetComponent<Mothman>();

    }

    private void ProcessPoltergeist(Monster_Data monsterData, GameObject monster)
    {
        Poltergeist poltergeist = monster.GetComponent<Poltergeist>();
        poltergeist.poltergeistObjects = poltergeistObjects;

    }

}
