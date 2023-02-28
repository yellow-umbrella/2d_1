using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    void Update()
    {
        transform.rotation = CalculateRotation(transform.position,
            Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                shotTime = Time.time + timeBetweenShots;
                Instantiate(projectile, shotPoint.position, transform.rotation);
            }
        }
    }

    static public Quaternion CalculateRotation(Vector3 position, Vector3 target)
    {
        Vector2 direction = target - position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        return rotation;
    }
}
