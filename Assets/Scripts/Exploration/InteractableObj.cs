using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    None,
    Fixed,
    Held,
    Thrown
}

public class InteractableObj : MonoBehaviour
{
    public GridLayer grid;
    
    [Multiline]
    public string text;
    
    public float groundedY;
    public ObjectState state;
    private bool throwing;
    private PlayerInteraction player;
    private Transform target;
    private Vector2Int gridPos;

    private new Camera camera;
    
    public float getGroundedY => groundedY;
    public Vector2Int Position => gridPos;

    private void Start()
    {
        camera = Camera.main;
        state = ObjectState.None;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        Vector3 actualWorldPosition = new Vector3(transform.position.x, grid.transform.position.y, transform.position.z);
        gridPos = grid.WorldToCell(actualWorldPosition);
        grid.Nodes[gridPos.y, gridPos.x].walkable = false;
    }

    private void Update()
    {
        switch (state)
        {
            case ObjectState.Fixed:
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
                {
                    target = info.transform;
                    Vector3 pos = info.transform.position;
                    pos.y = getGroundedY;
                    gridPos = grid.WorldToCell(pos);
                    
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
                        state = ObjectState.Thrown;
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
                        state = ObjectState.Thrown;
                    }

                    grid.Nodes[gridPos.y, gridPos.x].walkable = false;
                    player.Unit.speed = 16;
                }
                break;
            case ObjectState.Held:
                player.Unit.speed = 8;
                Vector3 newPos = player.transform.position;
                newPos.y = transform.position.y;
                transform.position = newPos;
                break;
            case ObjectState.None:
                break;
            case ObjectState.Thrown:
                if (target.gameObject.HasComponent<CombatHealth>() && target.GetComponent<CombatHealth>().Damage(5))
                    Destroy(target.gameObject);
                
                Destroy(gameObject);
                state = ObjectState.None;
                break;
        }
    }
}
