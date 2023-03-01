using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, ICanTakeDamage
{

    public event Action OnKilled;

    [SerializeField] protected int health;
    [SerializeField] protected float speed;

    public int pickupChance;
    public GameObject[] pickups;

    protected Player player;
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        player = Player.Instance;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            int randpmNumber = UnityEngine.Random.Range(0, 100);
            if (randpmNumber < pickupChance)
            {
                GameObject randomPickup = pickups[UnityEngine.Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
            OnKilled?.Invoke();
            //Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
