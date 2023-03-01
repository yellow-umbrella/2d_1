using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, ICanTakeDamage
{

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
            int randpmNumber = Random.Range(0, 100);
            if (randpmNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            //Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
