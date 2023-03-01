using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    private RangeAttack attack;

    protected override void Start()
    {
        base.Start();
        attack = GetComponent<RangeAttack>();
    }

    void Update()
    {
        if (player != null)
        {
            if (!attack.CloseToTarget(player.gameObject))
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (attack.CalculateTime())
                {
                    animator.SetTrigger("shoot");
                }
            }
        }
    }

    public void Shoot()
    {
        if (player != null)
        {
            attack.AttackTarget(player.gameObject);
        }
    }
}
