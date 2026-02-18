using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using NUnit.Framework;

public class Radar : MonoBehaviour
{
    public Enemy_Director enemyDirector;
    public Director director;
    public Vector2 playerPosition;
    public int enemyAppearingChances;
    public float attemptSpawnFrequency;
    public List<EnemyRadarCounterpart> activeEnemies;
    public List<EnemyRadarCounterpart> possibleEnemies;
    private void Start()
    {
        activeEnemies = new List<EnemyRadarCounterpart>();
        StartCoroutine(UpdateEnemyStates());
        
    }

    public IEnumerator UpdateEnemyStates()
    { 
        yield return new WaitForSeconds(attemptSpawnFrequency);
        AttemptEnemySpawn();
    }

    public void AttemptEnemySpawn()
    {

        int spawnChance = Random.Range(0, enemyAppearingChances * (activeEnemies.Count + 1));
        if(spawnChance == 1)
        {
            SpawnEnemyWandering();
        }
    }

    public void SpawnEnemyWandering()
    {
        List<Monster_Data> possibleMonsters = director.GetReadyMonsters();
        if(possibleMonsters.Count != 0)
        {
            int randomMonster = Random.Range(0, possibleMonsters.Count);
            Monster_Data chosenMonster = possibleMonsters[randomMonster];



            EnemyRadarCounterpart generatedMonster = new EnemyRadarCounterpart();
            generatedMonster.monsterType = chosenMonster.type;
            generatedMonster.currentPosition = FindSpawnPosition();
        }
    }

    public EnemyRadarCounterpart RetrieveEnemyData(Monster_Data chosenMonster)
    {
        foreach(EnemyRadarCounterpart enemy in possibleEnemies)
        {
            if(enemy.monsterType == chosenMonster.type)
            {
                return enemy;
            }
        }
        return null;
    }

    public Vector2 FindSpawnPosition()
    {
        return new Vector2(0, 0);
    }
}

public class EnemyRadarCounterpart
{

    public Monster_Data.MonsterType monsterType;
    public Vector2 currentPosition;
    public List<Vector2> targetPathPoints;
    public Vector2 targetPosition;
    public Vector2 speedRange;
    public float maxSpeed;
    public int dissapearingChances;
    public enum CurrentState { Running, Walking, Standing }
    public CurrentState state;
}
