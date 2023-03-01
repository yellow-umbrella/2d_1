using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private enum State
    {
        CountdownToStart,
        WavesOfEnemies,
        Boss,
        EndOfGame
    }

    public event Action OnGameOver;
    public event Action OnVictory;

    [SerializeField] private float countdownTimer;

    private State state;

    private void Awake()
    {
        Instance = this;
        state = State.CountdownToStart;
    }

    private void Update()
    {
        if (Player.Instance.Health <= 0)
        {
            state = State.EndOfGame;
            OnGameOver?.Invoke();
        }

        if (state == State.CountdownToStart)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                state = State.WavesOfEnemies;
            }
        }
    }

    private void AllWavesSpawned()
    {
        state = State.Boss;
    }

    private void BossKilled()
    {
        OnVictory?.Invoke();
    }
}
