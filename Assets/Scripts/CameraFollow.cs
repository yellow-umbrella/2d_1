using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform minCorner;
    public Transform maxCorner;
    public float speed;
    
    private Transform playerTransform;
    private Vector2 playerPosition;
    private Vector2 minPosition;
    private Vector2 maxPosition;

    void Start()
    {
        playerTransform = Player.Instance.transform;
        playerPosition = playerTransform.position;
        Vector3 cameraCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 delta = transform.position - cameraCorner;
        minPosition = minCorner.position + delta;
        maxPosition = maxCorner.position - delta;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position;
            float clampedX = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
            float clampedY = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);

            transform.position = new Vector3(clampedX, clampedY, 0);
            //transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
