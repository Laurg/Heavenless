using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountDown = 0;

    private SpawnState state = SpawnState.COUNTING;
    private int currentWave;


    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {

        if (state == SpawnState.WAITING)
        {
            // Check if all enemies are dead
            //Debug.Log(EnemiesAreDead());
            if (!EnemiesAreDead())
            {
                return;
            } else
            {
                // Complete wave 
                CompleteWave();
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Spawn enemies
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnDemon(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }

        state = SpawnState.WAITING;

        yield break;
    }


    // Variables
    [SerializeField] private Wave[] waves;

    // References
    [SerializeField] private Transform[] spawners;

    [SerializeField] private List<Character_Stats> enemyList;

    [SerializeField] private GameObject demon;

    private void SpawnDemon(GameObject enemy)
    {
        int randomInt = Random.Range(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];


        GameObject newEnemy =  Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);
        Character_Stats newEnemyStats = newEnemy.GetComponent<Character_Stats>();

        enemyList.Add(newEnemyStats);
    }

    private bool EnemiesAreDead()
    {
        int i = 0;
        foreach(Character_Stats enemy in enemyList)
        {
            if (enemy.IsDead())
            {
                i++;
            }
            else {
                return false;
            }
        }
        return true;
    }

    private void CompleteWave()
    {
        Debug.Log("WAVE COMPLETED!!!");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("ALL WAVES COMPLETED!!!");
        }
        else {
            // Next wave 
            Debug.Log("PREPARE FOR NEXT WAVE");
            currentWave++;
        }
    }
}
