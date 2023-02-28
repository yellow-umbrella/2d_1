using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : Creature
{
    public int pickupChance;
    public GameObject[] pickups;

    protected Player player;

    protected override void Start()
    {
        base.Start();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
    }

    public override bool TakeDamage(int amount)
    {
        bool died = base.TakeDamage(amount);
        if (died)
        {
            int randpmNumber = Random.Range(0, 100);
            if (randpmNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
        }
        return died;
    }
}
