using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Summoner : Enemy
{
    [Header("Summon Settings")]
    public float summonCooldown;
    public Enemy enemyToSummon;
    [Space(10)]
    public Vector2 minCorner;
    public Vector2 maxCorner;

    private Vector2 targetPosition;
    private float nextSummonTime;

    private MeleeAttack attack;

    protected override void Start()
    {
        base.Start();
        attack = GetComponent<MeleeAttack>();

        float randomX = Random.Range(minCorner.x, maxCorner.x);
        float randomY = Random.Range(minCorner.y, maxCorner.y);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, targetPosition, speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            } else
            {
                animator.SetBool("isRunning", false);
                if (Time.time >= nextSummonTime)
                {
                    nextSummonTime = Time.time + summonCooldown;
                    animator.SetTrigger("summon");
                } else
                {
                    if (attack.CloseToTarget(player.gameObject) && attack.CalculateTime())
                    {
                        attack.AttackTarget(player.gameObject);
                    }
                }
            }
        }
    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

}
