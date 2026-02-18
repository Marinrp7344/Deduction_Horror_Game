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

    public float mapRadius;
    public float triggerDistance;
    public float minDistance;
    public float maxDistance;
    public Transform radarUIParent;
    public GameObject radarIndicatorPrefab;
    public GameObject radarUI;

    private void Start()
    {
        activeEnemies = new List<EnemyRadarCounterpart>();
        StartCoroutine(UpdateEnemyStates());
        
    }
    private void Update()
    {
        MoveEnemies();
    }

    public void MoveEnemies()
    {
        foreach (EnemyRadarCounterpart enemy in activeEnemies)
        {
            if (enemy.state == EnemyRadarCounterpart.CurrentState.Standing)
            {
                Debug.Log("Standing");
                continue;
            }
            Vector2 direction = (enemy.targetPosition - enemy.currentPosition);
            float distance = direction.magnitude;

            if (distance > 0.1f)
            { 

                direction.Normalize();

                float speed = GetSpeedFromState(enemy);

                enemy.currentPosition += direction * speed * Time.deltaTime;
            }
            else
            {
                enemy.reachedPosition = true;
            }

            ClampToRadarBounds(enemy);
            CheckIfCloseToPlayer(enemy);
        }
    }

    float GetSpeedFromState(EnemyRadarCounterpart enemy)
    {
        float baseSpeed = Random.Range(enemy.speedRange.x, enemy.speedRange.y);

        switch (enemy.state)
        {
            case EnemyRadarCounterpart.CurrentState.Standing:
                return 0.1f;

            case EnemyRadarCounterpart.CurrentState.Walking:
                return baseSpeed * 0.5f;

            case EnemyRadarCounterpart.CurrentState.Running:
                return baseSpeed * 1.5f;
        }

        return baseSpeed;
    }

    public IEnumerator UpdateEnemyStates()
    {
        while (true)
        {
            yield return new WaitForSeconds(attemptSpawnFrequency);

            AttemptEnemySpawn();
            UpdateActiveEnemies();
        }
    }

    public void UpdateActiveEnemies()
    {
        foreach (EnemyRadarCounterpart enemy in activeEnemies)
        {
            if (enemy.reachedPosition)
            {

                Vector2 toPlayer = (playerPosition - enemy.currentPosition).normalized;

                float decision = Random.value;
                Vector2 chosenDirection;

                if (decision < 0.15f)
                {
                    chosenDirection = Random.insideUnitCircle.normalized;
                    enemy.state = EnemyRadarCounterpart.CurrentState.Walking;
                }
                else if (decision < 0.25f)
                {
                    Vector2 awayFromPlayer = -toPlayer;
                    chosenDirection = (awayFromPlayer + Random.insideUnitCircle).normalized;
                    enemy.state = EnemyRadarCounterpart.CurrentState.Walking;
                }
                else if (decision < 0.75f)
                {
                    chosenDirection = (toPlayer + Random.insideUnitCircle * 0.5f).normalized;
                    enemy.state = EnemyRadarCounterpart.CurrentState.Walking;
                }
                else
                {
                    chosenDirection = toPlayer;
                    enemy.state = EnemyRadarCounterpart.CurrentState.Running;
                }

                float moveDistance = Random.Range(minDistance, maxDistance);

                Vector2 rawTarget = enemy.currentPosition + chosenDirection * moveDistance;

                enemy.targetPosition = ClampPositionToRadar(rawTarget);
                enemy.reachedPosition = false;
            }
        }
    }

    Vector2 ClampPositionToRadar(Vector2 position)
    {
        if (position.magnitude > mapRadius)
        {
            return position.normalized * mapRadius;
        }

        return position;
    }

    void ClampToRadarBounds(EnemyRadarCounterpart enemy)
    {
        if (enemy.currentPosition.magnitude > mapRadius)
        {
            enemy.currentPosition = enemy.currentPosition.normalized * mapRadius;
        }
    }

    void CheckIfCloseToPlayer(EnemyRadarCounterpart enemy)
    {
        float distance = Vector2.Distance(enemy.currentPosition, playerPosition);

        if (distance < triggerDistance)
        {
            enemyDirector.SpawnMonster(enemy.monster);
            activeEnemies.Remove(enemy);
            Destroy(enemy.indicator);
        }
    }


    public void AttemptEnemySpawn()
    {
        if (activeEnemies.Count >= 2)
            return;

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

            EnemyRadarCounterpart generatedMonster = RetrieveEnemyData(chosenMonster);
            activeEnemies.Add(generatedMonster);
            generatedMonster.currentPosition = FindSpawnPosition();
            GameObject indicator = Instantiate(radarIndicatorPrefab, transform.position, Quaternion.identity);
            RadarUI_MonsterIndicator indicatorUI = indicator.GetComponent<RadarUI_MonsterIndicator>();
            indicatorUI.monsterCounterPart = generatedMonster;
            indicatorUI.parent = radarUIParent;
            generatedMonster.indicator = indicator;
        }
    }

    public EnemyRadarCounterpart RetrieveEnemyData(Monster_Data chosenMonster)
    {
        foreach(EnemyRadarCounterpart enemy in possibleEnemies)
        {
            if(enemy.monsterType == chosenMonster.type)
            {
                Debug.Log("Monster Spawned: " + enemy.monsterType.ToString());
                return enemy;
            }
        }
        return null;
    }

    public Vector2 FindSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * mapRadius;
        float y = Mathf.Sin(angle) * mapRadius;

        return new Vector2(x, y);
    }

    public void ActivateRadar()
    {
        radarUI.SetActive(true);

        foreach(EnemyRadarCounterpart enemy in activeEnemies)
        {
            if (enemy.indicator != null)
            {
                RadarUI_MonsterIndicator indicatorUI = enemy.indicator.GetComponent<RadarUI_MonsterIndicator>();
                indicatorUI.transform.SetParent(radarUIParent);
            }
        }
    }

    public void DeactivateRadar()
    {
        radarUI.SetActive(false);
    }
}

[System.Serializable]
public class EnemyRadarCounterpart
{

    public Monster_Data.MonsterType monsterType;
    public Monster_Data monster;
    public Vector2 currentPosition;
    public Vector2 targetPosition;
    public Vector2 speedRange;
    public bool reachedPosition = true;
    public GameObject indicator;
    //public float maxSpeed;
    //public int dissapearingChances;
    public enum CurrentState { Running, Walking, Standing }
    public CurrentState state;
}
