using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    public GameObject boss;
    public Transform bossSpawnPoint;
    public GameObject bossHealthBar;

    private int currentWave;
    private Transform player;
    private bool finishedSpawning;

    private GameObject bossInstance;
    private bool spawnedBoss;

    private void Start()
    {
        player = Player.Instance.gameObject.transform;
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[currentWave];
        for (int i = 0; i < wave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }
            Enemy enemy = wave.enemies[Random.Range(0, wave.enemies.Length)];
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, point.position, Quaternion.identity);

            if (i == wave.count - 1)
            {
                finishedSpawning = true;
            } else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if (spawnedBoss && bossInstance == null && 
            GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //victoryHandler.EndGame();
            gameObject.SetActive(false);
        }

        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWave + 1 < waves.Length)
            {
                currentWave++;
                StartCoroutine(StartNextWave());
            } else
            {
                bossInstance = Instantiate(boss, bossSpawnPoint.position, Quaternion.identity);
                bossHealthBar.SetActive(true);
                spawnedBoss = true;
            }
        }
    }

}
