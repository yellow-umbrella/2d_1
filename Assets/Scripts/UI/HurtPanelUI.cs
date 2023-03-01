using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPanelUI : MonoBehaviour
{
    private const string ANIM_HURT = "hurt";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnHurt += Player_OnHurt;
        Hide();
    }

    private void Player_OnHurt()
    {
        Show();
        animator.SetTrigger(ANIM_HURT);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
