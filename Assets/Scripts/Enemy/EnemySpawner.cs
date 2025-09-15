using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // list of enemy groups in this wave
        public int waveQuota; // the total number of enemies to spawn in this wave
        public float spawnInterval; // the interval between spawns
        public int spawnCount; // number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnedCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // list of waves
    public int currentWaveCount; // the index of the current wave [remember, a list starts from 0]

    [Header("Spawner Attributes")]
    float spawnTimer; // timer to track spawn intervals
    public int enemiesAlive;
    public int maxEnemiesAllowed; // maximum number of enemies allowed at once
    public bool maxEnemiesReached = false; // flag to indicate if max enemies are reached
    public float waveInterval; // interval between waves


    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //A list to store all the relative spawn points


    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuata();
    }

    void Update()
    {

        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //check if the wave has ended and the next wave should start
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        //Check if it's time to spawn enemies
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

    }

    IEnumerator BeginNextWave()
    {
        //wave for 'waveInterval' seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        //if there are more waves to spawn, increment the wave count and calculate the new wave quota
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuata();
        }
    }

    void CalculateWaveQuata()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    /// <summary>
    /// This method will stop spawning enemies if the amount of enemies on the map is maximu,.
    /// The method will only spawn enemies in a particular wave until it is time for next wave's enemies to spawn.
    /// </summary>
    void SpawnEnemies()
    {
        //check if the minimum number of enemies for the wave has been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawn enemies from each group in the wave
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //check if the enemy group has spawned all its enemies
                if (enemyGroup.spawnedCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        //Limit the number of enemies that can be spaswned at once
                        maxEnemiesReached = true;
                        return; //exit the function if max enemies are reached
                    }

                    //Spawn the enemy at a random position close to the player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);


                    enemyGroup.spawnedCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        //reset the spawned count for each enemy group if the wave is complete
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //call this function when an enemy is killed
    public void OnenemyKilled()
    {
        //decrement the number of alive enemies when an enemy is killed
        enemiesAlive--;
    }
}
