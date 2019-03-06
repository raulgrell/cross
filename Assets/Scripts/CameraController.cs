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
    internal Transform selected;
    
    void Start()
    {
        camera = GetComponent<Camera>();
        offset = (player == null )
            ? transform.position 
            : transform.position - player.transform.position;
        direction = transform.eulerAngles;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                selected = hitInfo.transform;
            }
        }
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
        desiredPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (player)
            Gizmos.DrawLine(transform.position, player.transform.position);
        
        if (selected)
        {
            Gizmos.DrawWireCube(selected.position, Vector3.one);
        }
    }
}
