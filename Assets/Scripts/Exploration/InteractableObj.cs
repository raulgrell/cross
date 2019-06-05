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
    public float groundedY;
    public ObjectState state;
    
    [Multiline]
    public string text;
    
    internal GridLayer grid;
    private bool throwing;
    private PlayerInteraction player;
    private Transform target;
    private Vector2Int gridPos;
    private GridUnit unit;
    private bool throwned;

    private new Camera camera;
    
    public float getGroundedY => groundedY;
    public Vector2Int Position => gridPos;
    public Vector2Int SetPosition { set => gridPos = value; }

    private void Start()
    {
        unit = GetComponent<GridUnit>();
        grid = FindObjectOfType<GridLayer>();
        camera = Camera.main;
        state = ObjectState.None;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        Vector3 actualWorldPosition = new Vector3(transform.position.x, grid.transform.position.y, transform.position.z);
        gridPos = grid.WorldToCell(actualWorldPosition);
        grid.Nodes[gridPos.y, gridPos.x].unit = unit;
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
                    grid.Nodes[gridPos.y, gridPos.x].unit = null;

                    gridPos = grid.WorldToCell(pos);
                    
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
                        throwned = true;               
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
                        throwned = false;               
                    }

                    if(Vector3.Distance(transform.position,pos) < 0.1f)
                    {
                        if (throwned)
                            state = ObjectState.Thrown;
                        else
                            state = ObjectState.None;
                    }

                    grid.Nodes[gridPos.y, gridPos.x].unit = unit;
                    player.Unit.speed = 16;
                }
                break;
            case ObjectState.Held:
                player.Unit.speed = 8;
                Vector3 newPos = player.transform.position;
                newPos.y = transform.position.y;
                transform.position = newPos;
                grid.Nodes[gridPos.y, gridPos.x].unit = null;
                break;
            case ObjectState.None:
                grid.Nodes[gridPos.y, gridPos.x].unit = unit;
                break;
            case ObjectState.Thrown:
                if (target.gameObject.HasComponent<CombatHealth>() && target.GetComponent<CombatHealth>().Damage(5))
                    Destroy(target.gameObject);
                grid.Nodes[gridPos.y, gridPos.x].unit = null;

                Destroy(gameObject);
                state = ObjectState.None;
                break;
        }
    }
}
