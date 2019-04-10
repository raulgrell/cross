using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public GridLayer grid;
    public string text;
    public float groundedY;
    internal int state = 2;
    private PlayerInteraction player;

    public float getGroundedY => groundedY;
    public Vector2Int getGridPos { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
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
                    if (info.transform.CompareTag("Tile"))
                    {
                        Debug.Log(Vector2.Distance(player.gridUnit.position, getGridPos));
                        if (Vector2.Distance(player.gridUnit.position, getGridPos) <= 3)
                        {
                            Vector3 newPos = info.transform.position;
                            newPos.y = transform.position.y;
                            getGridPos = grid.WorldToCell(newPos);
                            transform.position = Vector3.Lerp(transform.position, newPos, 1000);
                        }
                    }
                }
                break;
        }
    }
}
