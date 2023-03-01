using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    private enum State
    {
        Idle,
        WaitingForNextWave,
        SpawningWave,
        WaitingForDefeatingWave,
        Boss
    }

    [SerializeField] private WaveSO[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves;

    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private GameObject bossHealthBar;

    private State state;

    private int currentWave;
    private int enemiesSpawned;
    private float timer;

    private int aliveEnemiesCount;
    private GameObject bossInstance;

    private Action allWavesDefeatedCallback;
    private Action bossDefeatedCallback;

    private void Awake()
    {
        Instance = this;
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.WaitingForNextWave:
                WaitingForNextWave();
                break;
            case State.SpawningWave:
                SpawningWave();
                break;
            case State.WaitingForDefeatingWave:
                if (aliveEnemiesCount <= 0)
                {
                    state = State.WaitingForNextWave;
                    InitTimer(timeBetweenWaves);
                    currentWave++;
                }
                break;
            case State.Boss:
                if (aliveEnemiesCount <= 0 && bossInstance == null)
                {
                    state = State.Idle;
                    bossDefeatedCallback();
                }
                break;
        }
    }

    private void WaitingForNextWave()
    {
        if (currentWave == waves.Length)
        {
            state = State.Idle;
            allWavesDefeatedCallback();
            return;
        }

        if (TickTimer())
        {
            state = State.SpawningWave;
            InitTimer(0);
            enemiesSpawned = 0;
        }
    }

    private void SpawningWave()
    {
        if (TickTimer())
        {
            InitTimer(waves[currentWave].timeBetweenSpawns);
            SpawnEnemy();
            enemiesSpawned++;
            if (enemiesSpawned == waves[currentWave].count)
            {
                state = State.WaitingForDefeatingWave;
            }
        }
    }

    public void SpawnWaves(Action callback)
    {
        state = State.SpawningWave;
        InitTimer(0);
        allWavesDefeatedCallback = callback;
    }
    
    public void SpawnBoss(Action callback)
    {
        bossInstance = Instantiate(boss, bossSpawnPoint.position, Quaternion.identity);
        bossHealthBar.SetActive(true);
        state = State.Boss;
        bossDefeatedCallback = callback;
    }

    public void SpawnEnemy(Enemy enemy, Vector2 position, Quaternion rotation)
    {
        aliveEnemiesCount++;
        Instantiate(enemy, position, rotation).OnKilled += KillEnemy;
    }

    private void SpawnEnemy()
    {
        WaveSO wave = waves[currentWave];
        Enemy enemy = wave.enemies[UnityEngine.Random.Range(0, wave.enemies.Length)];
        Transform point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        SpawnEnemy(enemy, point.position, Quaternion.identity);
    }
    
    private void InitTimer(float maxTime)
    {
        timer = maxTime;
    }

    private bool TickTimer()
    {
        timer -= Time.deltaTime;
        return timer <= 0;
    }

    private void KillEnemy()
    {
        aliveEnemiesCount--;
    }
}
