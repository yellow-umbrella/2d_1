using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ICanTakeDamage
{
    public static Player Instance { get; private set; }

    public event Action OnHurt;
    public event Action OnHealed;

    [SerializeField] private int health;
    [SerializeField] private float speed;

    [SerializeField] private EndGameHandler gameOverHandler;
    [SerializeField] private Transform wandHolder;
    [SerializeField] private GameObject weapon;
    
    public Vector2 MoveVector => moveVector;
    public int Health => health;
    public int MaxHealth => maxHealth;

    //[SerializeField] private Animator hurtAnimator;

    private Rigidbody2D rb;
    private Vector2 moveVector;
    private int maxHealth;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        moveVector = inputVector * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Destroy(weapon);
        weapon = Instantiate(newWeapon, wandHolder).gameObject;
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        OnHealed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHurt?.Invoke();
        //hurtAnimator.SetTrigger("hurt");
        if (health <= 0)
        {
            Destroy(gameObject);
            gameOverHandler.EndGame();
        }
    }
}
