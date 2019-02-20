using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 bossVector;
    private Vector3 offset;
    private Camera camera;
    float initialpositiony;

    public float smoothSpeed = 0.125f;

    void Start()
    { 
        offset = transform.position - player.transform.position;
        initialpositiony = transform.position.y;
    }

    void LateUpdate()
    {
        if (player != null)
        {
                Vector3 desiredPosition = player.transform.position + offset;
                transform.position = desiredPosition;
        }
    }
}
