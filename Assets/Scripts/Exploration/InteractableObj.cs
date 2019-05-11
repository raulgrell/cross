using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public GridLayer grid;
    public string text;
    public float groundedY;
    internal int state = 2;
    internal bool throwing;
    private PlayerInteraction player;
    private Transform target;

    public float getGroundedY => groundedY;
    public Vector2Int getGridPos { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        Vector3 actualWorldPosition = new Vector3(transform.position.x, grid.transform.position.y, transform.position.z);
        getGridPos = grid.WorldToCell(actualWorldPosition);
        grid.nodes[getGridPos.x, getGridPos.y].walkable = false;
    }

    private void Update()
    {

        switch (state)
        {
            case 0:
                Vector3 pos = new Vector3();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
                {
                    target = info.transform;
                    pos = info.transform.position;
                    pos.y = getGroundedY;
                    getGridPos = grid.WorldToCell(pos);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
                        throwing = true;
                    }
                    else
                        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);

                    grid.nodes[getGridPos.x, getGridPos.y].walkable = false;
                    player.gridUnit.speed = 16;
                }
                if (transform.position == pos)
                    if (throwing)
                    {
                        state = 3;
                    }
                    else
                        state = 2;
                break;
            case 1:
                player.gridUnit.speed = 8;
                getGridPos = player.gridUnit.position;
                Vector3 newPos = grid.CellToWorld(getGridPos);
                newPos.y = transform.position.y;
                transform.position = newPos;
                break;
            case 2:

                break;

            case 3:

                if (target.CompareTag("Enemy"))
                {
                    if (target.gameObject.HasComponent<CombatHealth>())
                        if (target.GetComponent<CombatHealth>().Damage(5))
                            Destroy(target.gameObject);
                    Destroy(gameObject);
                }
                else
                   Destroy(gameObject);
                throwing = false;
                state = 2;
                break;
        }
        
    }
}
