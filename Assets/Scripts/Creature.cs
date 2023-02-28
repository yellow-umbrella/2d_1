using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject deathEffect;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual bool TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
