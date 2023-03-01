using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.ChangeWeapon(weaponToEquip);
            Instantiate(effect, collision.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
