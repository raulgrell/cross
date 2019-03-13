using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed = 16;
    public float horizontalSway = -2;
    public float verticalSway = 8;
    public float horizontalRotation = -8;
    public float verticalRotation = -2;
    
    private Vector3 offset;
    private Vector3 direction;
    private new Camera camera;
    
    void Start()
    {
        var initialPosition = transform.position;
        camera = GetComponent<Camera>();
        offset = (player == null )
            ? initialPosition 
            : initialPosition - player.transform.position;
        direction = transform.eulerAngles;
    }

    void LateUpdate()
    {
        if (player == null) return;

        var mousePos = Input.mousePosition;
        mousePos.x = 2 * (mousePos.x / Screen.width - 0.5f);
        mousePos.y = 2 * (mousePos.y / Screen.height - 0.5f);
        
        var mouseOffset = Vector3.right * mousePos.x * horizontalSway + Vector3.forward * mousePos.y * verticalSway;
        var mouseRotation = Quaternion.Euler(direction.x + mousePos.y * verticalRotation, mousePos.x * horizontalRotation, 0);
        transform.rotation = mouseRotation;
        
        Vector3 desiredPosition = player.transform.position + offset + mouseOffset;
        Vector3 currentPosition = transform.position;
        desiredPosition.y = currentPosition.y;

        transform.position = Vector3.MoveTowards(currentPosition, desiredPosition, cameraSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (player)
            Gizmos.DrawLine(transform.position, player.transform.position);
    }
}
