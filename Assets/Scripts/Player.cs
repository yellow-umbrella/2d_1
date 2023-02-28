using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Creature
{
    [SerializeField] private EndGameHandler gameOverHandler;

    public Transform wandHolder;
    public GameObject weapon;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator hurtAnimator;

    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private int maxHealth;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    void Update()
    {
        Vector2 moveInput = 
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        if (moveAmount != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.deltaTime);
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        Destroy(weapon);
        weapon = Instantiate(newWeapon, wandHolder).gameObject;
    }

    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public override bool TakeDamage(int amount)
    {
        bool died = base.TakeDamage(amount);
        UpdateHealthUI(health);
        hurtAnimator.SetTrigger("hurt");
        if (died)
        {
            gameOverHandler.EndGame();
        }
        return died;
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthUI(health);
    }
}
