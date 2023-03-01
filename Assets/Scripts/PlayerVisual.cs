using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string ANIM_RUNNING_BOOL = "isRunning";

    [SerializeField] private GameObject deathEffect;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnHurt += Player_OnHurt;
    }

    private void Player_OnHurt()
    {
        if (Player.Instance.Health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    private void Update()
    {
        animator.SetBool(ANIM_RUNNING_BOOL, Player.Instance.MoveVector != Vector2.zero);
    }
}
