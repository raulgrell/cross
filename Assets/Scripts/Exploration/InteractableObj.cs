using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public GridLayer grid;
    public string text;
    public float groundedY;
    internal int state = 2;

    public float getGroundedY => groundedY;
    public Vector2Int getGridPos { get; set; }

    private void Start()
    {
        Vector3 actualWorldPosition = new Vector3(transform.position.x, grid.transform.position.y, transform.position.z);
        getGridPos = grid.WorldToCell(actualWorldPosition);
    }

    private void Update()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
                {
                    if (info.transform.CompareTag("Ground"))
                    {
                        Vector3 newPos = info.transform.position;
                        newPos.y = transform.position.y;
                        transform.position = Vector3.Lerp(transform.position, newPos, 1000);
                       
                    }
                }
                break;
        }
    }
}
