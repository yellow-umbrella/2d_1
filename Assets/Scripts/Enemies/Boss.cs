using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Creature
{
    public int damage;

    public Enemy[] enemies;
    public Vector3 spawnOffset;

    private int halfHealth;
    private Animator cameraAnimator;
    private Slider healthBar;

    protected override void Start()
    {
        base.Start();
        halfHealth = health / 2;
        cameraAnimator = Camera.main.GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public override bool TakeDamage(int amount)
    {
        bool died = base.TakeDamage(amount);
        if (!died)
        {
            if (health <= halfHealth)
            {
                animator.SetTrigger("stage2");
            }
            Enemy enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, transform.position + spawnOffset, transform.rotation);
            healthBar.value = health;
        } else
        {
            healthBar.gameObject.SetActive(false);
        }
        return died;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void ShakeCamera()
    {
        cameraAnimator.SetTrigger("shake");
    }

    public void ShakeCameraLight()
    {
        cameraAnimator.SetTrigger("shakeLight");
    }
}
