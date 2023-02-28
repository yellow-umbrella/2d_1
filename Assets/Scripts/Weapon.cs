using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    private void Start()
    {
        GameInput.Instance.OnFireAction += GameInput_OnFireAction;
    }

    private void GameInput_OnFireAction()
    {
        if (Time.time >= shotTime)
        {
            shotTime = Time.time + timeBetweenShots;
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }
    }

    void Update()
    {
        transform.rotation = CalculateRotation(transform.position,
            GameInput.Instance.GetMousePosition());
    }

    static public Quaternion CalculateRotation(Vector3 position, Vector3 target)
    {
        Vector2 direction = target - position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        return rotation;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnFireAction -= GameInput_OnFireAction;
    }
}
