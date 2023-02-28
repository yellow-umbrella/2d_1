using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{   
    private MeleeAttack attack;

    protected override void Start()
    {
        base.Start();
        attack = GetComponent<MeleeAttack>();
    }

    void Update()
    {
        if (player != null)
        {
            if (attack.CloseToTarget(player))
            {
                if (attack.CalculateTime())
                {
                    attack.AttackTarget(player);
                }
            } else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, player.transform.position, speed * Time.deltaTime);
            }

            SetRotation();
        }
    }

    private void SetRotation()
    {
        if (player != null)
        {
            float directionCoef = 1;
            if (player.transform.position.x < transform.position.x)
            {
                directionCoef = -1;
            }
            transform.localScale = new Vector3(
                    directionCoef * Mathf.Abs(transform.localScale.x), 
                    transform.localScale.y, transform.localScale.z);
        }
    }
}
