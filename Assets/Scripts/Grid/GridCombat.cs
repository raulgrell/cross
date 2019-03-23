using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Attack
{
    public Vector2Int[] positions;

    public Attack(Vector2Int[] pos)
    {
        positions = pos;
    }
}

public class GridCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject specialPrefab;
    public new Camera camera;
    public float rotationSpeed;
    private GridLayer grid;
    private GridUnit gridUnit;

    private List<GridNode> neighbors;

    private Transform target;
    private bool targeting;
    private Attack meleeAttack;


    void Start()
    {
        gridUnit = GetComponent<GridUnit>();
        grid = gridUnit.grid;
    }

    void Update()
    {
        //Attacks
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(meleePrefab, transform.position + (transform.forward * 2), transform.rotation);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 projectilePos = transform.position;
            projectilePos.y = transform.position.y + 1.5f;
            Instantiate(projectilePrefab, projectilePos + (transform.forward), transform.rotation);
        }

        //Targeting System
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0.2f;
            targeting = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Time.timeScale = 1f;
            targeting = false;
        }

        if (targeting && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            target = hitInfo.transform;
        }

        if (target)
        {
            Vector3 newTarget = target.position;
            newTarget.y = transform.position.y;
            rotateTowards(newTarget);
        }
    }

    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(target.position, Vector3.one);
            Gizmos.DrawLine(transform.position, target.position);

            DrawTarget(transform.eulerAngles.y);
        }
    }

    void rotateTowards(Vector3 direction)
    {
        var newRotation = Quaternion.LookRotation(-1 * (transform.position - direction), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }


    void DrawTarget(float rotation)
    {
        Vector2Int newPos = new Vector2Int();
        Vector2Int direction = gridUnit.position;
        Vector2Int[] closeAttacks = new Vector2Int[3];
        
        if (rotation <= 23 && rotation > 0 || rotation > 337 && rotation <= 360)
        {
            closeAttacks[0] = new Vector2Int(direction.x, direction.y + 1);
            closeAttacks[1] = new Vector2Int(direction.x + 1, direction.y + 1);
            closeAttacks[2] = new Vector2Int(direction.x - 1, direction.y + 1);

            newPos = new Vector2Int(direction.x, direction.y + 1);
        }
        else if (rotation <= 181 && rotation > 158)
        {
            closeAttacks[0] = new Vector2Int(direction.x, direction.y - 1);
            closeAttacks[1] = new Vector2Int(direction.x + 1, direction.y - 1);
            closeAttacks[2] = new Vector2Int(direction.x - 1, direction.y - 1);

            newPos = new Vector2Int(direction.x, direction.y - 1);
        }
        else if (rotation <= 158 && rotation > 113)
        {
            closeAttacks[0] = new Vector2Int(direction.x + 1, direction.y - 1);
            closeAttacks[1] = new Vector2Int(direction.x + 1, direction.y);
            closeAttacks[2] = new Vector2Int(direction.x, direction.y - 1);

            newPos = new Vector2Int(direction.x + 1, direction.y - 1);
        }
        else if (rotation <= 113 && rotation > 68)
        {
            closeAttacks[0] = new Vector2Int(direction.x + 1, direction.y);
            closeAttacks[1] = new Vector2Int(direction.x + 1, direction.y + 1);
            closeAttacks[2] = new Vector2Int(direction.x + 1, direction.y - 1);

            newPos = new Vector2Int(direction.x + 1, direction.y);
        }
        else if (rotation <= 68 && rotation > 23f)
        {
            closeAttacks[0] = new Vector2Int(direction.x + 1, direction.y + 1);
            closeAttacks[1] = new Vector2Int(direction.x, direction.y + 1);
            closeAttacks[2] = new Vector2Int(direction.x + 1, direction.y);

            newPos = new Vector2Int(direction.x + 1, direction.y + 1);
        }
        else if (rotation <= 337 && rotation > 270)
        {
            closeAttacks[0] = new Vector2Int(direction.x - 1, direction.y + 1);
            closeAttacks[1] = new Vector2Int(direction.x, direction.y + 1);
            closeAttacks[2] = new Vector2Int(direction.x - 1, direction.y);

            newPos = new Vector2Int(direction.x - 1, direction.y + 1);
        }
        else if (rotation <= 270 && rotation > 225)
        {
            closeAttacks[0] = new Vector2Int(direction.x - 1, direction.y);
            closeAttacks[1] = new Vector2Int(direction.x - 1, direction.y + 1);
            closeAttacks[2] = new Vector2Int(direction.x - 1, direction.y - 1);

            newPos = new Vector2Int(direction.x - 1, direction.y);
        }
        else if (rotation <= 225 && rotation > 181)
        {
            closeAttacks[0] = new Vector2Int(direction.x - 1, direction.y - 1);
            closeAttacks[1] = new Vector2Int(direction.x - 1, direction.y);
            closeAttacks[2] = new Vector2Int(direction.x, direction.y - 1);

            newPos = new Vector2Int(direction.x - 1, direction.y - 1);
        }

        meleeAttack = new Attack(closeAttacks);
        doAttack(meleeAttack);

        Vector3 forwardPosition = grid.CellToWorld(newPos);
        forwardPosition.y = transform.position.y + 1;
        Gizmos.DrawSphere(forwardPosition, 0.5f);
    }

    void doAttack(Attack attack)
    {
        for (int i = 0; i < attack.positions.Length; i++)
        {
            Vector3 actualPosition = grid.CellToWorld(attack.positions[i]);
            actualPosition.y = transform.position.y;
            Gizmos.DrawWireCube(actualPosition, Vector3.one);
        }
    }
}