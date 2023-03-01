using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangeAttack : Attack
{
    public GameObject projectile;
    public Transform startPoint;

    public override void AttackTarget(GameObject target)
    {
        GameObject instance = Instantiate(projectile, startPoint.position,
            Weapon.CalculateRotation(startPoint.position, target.transform.position));
        Projectile projectileScr;
        if (instance.TryGetComponent(out projectileScr))
        {
            projectileScr.range = range;
            projectileScr.damage = damage;
        }
    }
}
