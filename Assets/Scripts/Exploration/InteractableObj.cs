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

    private new Camera camera;
    
    public float getGroundedY => groundedY;
    public Vector2Int getGridPos { get; set; }

    private void Start()
    {
        camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        Vector3 actualWorldPosition = new Vector3(transform.position.x, grid.transform.position.y, transform.position.z);
        getGridPos = grid.WorldToCell(actualWorldPosition);
        grid.Nodes[getGridPos.y, getGridPos.x].walkable = false;
    }

    private void Update()
    {
        switch (state)
        {
            case 0:
                Vector3 pos = new Vector3();
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
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

                    grid.Nodes[getGridPos.y, getGridPos.x].walkable = false;
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
                Vector3 newPos = player.transform.position;
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
