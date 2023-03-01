using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.Heal(healAmount);
            Instantiate(effect, collision.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
