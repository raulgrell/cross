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
        
        // t, tr, r, br, b, bl, l, tl
        var neighbours = new Vector2Int[8];
        neighbours[0] = new Vector2Int(direction.x + 0, direction.y + 1);
        neighbours[1] = new Vector2Int(direction.x + 1, direction.y + 1);
        neighbours[2] = new Vector2Int(direction.x + 1, direction.y + 0);
        neighbours[3] = new Vector2Int(direction.x + 1, direction.y - 1);
        neighbours[4] = new Vector2Int(direction.x + 0, direction.y - 1);
        neighbours[5] = new Vector2Int(direction.x - 1, direction.y - 1);
        neighbours[6] = new Vector2Int(direction.x - 1, direction.y + 0);
        neighbours[7] = new Vector2Int(direction.x - 1, direction.y + 1);

        var sectors = new[]
        {
            new[] {7, 0, 1},
            new[] {0, 1, 2},
            new[] {1, 2, 3},
            new[] {2, 3, 4},
            new[] {3, 4, 5},
            new[] {4, 5, 6},
            new[] {5, 6, 7},
            new[] {6, 7, 0},
        };

        int sector = (int)((rotation + 22.5f) / 45f) % 8;

        var threatened = new []
        {
            neighbours[sectors[sector][0]],
            neighbours[sectors[sector][1]],
            neighbours[sectors[sector][2]]
        };

        meleeAttack = new Attack(threatened);
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