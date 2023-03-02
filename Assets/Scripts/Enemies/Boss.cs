using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Vector3 spawnOffset;

    public float Speed => speed;

    private int halfHealth;
    private Animator cameraAnimator;
    private Slider healthBar;
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
        halfHealth = health / 2;
        cameraAnimator = Camera.main.GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            healthBar.gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            if (health <= halfHealth)
            {
                animator.SetTrigger("stage2");
            }
            Enemy enemy = enemies[Random.Range(0, enemies.Length)];
            EnemySpawner.Instance.SpawnEnemy(enemy, transform.position + spawnOffset, transform.rotation);
            healthBar.value = health;
        }
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
