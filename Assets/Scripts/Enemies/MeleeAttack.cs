using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public override void AttackTarget(Creature target)
    {
        StartCoroutine(AttackRoutine(target));
    }

    private IEnumerator AttackRoutine(Creature target)
    {
        target.TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = target.transform.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * speed;
            float t = -4 * Mathf.Pow(percent - 0.5f, 2) + 1; //parabola based interpolation
            transform.position = Vector2.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }
    }
}
