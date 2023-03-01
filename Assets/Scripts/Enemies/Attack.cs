using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float range;
    public float speed;
    public float cooldown;

    protected float nextAttackTime;

    public bool CloseToTarget(GameObject target)
    {
        return (Vector2.Distance(transform.position, target.transform.position) <= range);
    }

    public bool CalculateTime()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + cooldown;
            return true;
        }
        return false;
    }

    public virtual void AttackTarget(GameObject target)
    {
        return;
    }
}
