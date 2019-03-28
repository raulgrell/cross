using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackType
{
    Melee,
    Ranged,
    Special
}

class BasicAttack
{
    public Vector2Int centerPoint;
    public int[] positions;
    public float speed;

    public BasicAttack(Vector2Int center,int[] pos, float speed)
    {
        positions = pos;
        this.speed = speed;
        centerPoint = center;
    }

    public Vector2Int[] doAttack(float rotation, int range, AttackType type)
    {
        Vector2Int newPos = new Vector2Int();
        Vector2Int direction = centerPoint;
        var neighbours = new Vector2Int[8];
        neighbours[0] = new Vector2Int(direction.x + 0, direction.y + range);
        neighbours[1] = new Vector2Int(direction.x + range, direction.y + range);
        neighbours[2] = new Vector2Int(direction.x + range, direction.y + 0);
        neighbours[3] = new Vector2Int(direction.x + range, direction.y - range);
        neighbours[4] = new Vector2Int(direction.x + 0, direction.y - range);
        neighbours[5] = new Vector2Int(direction.x - range, direction.y - range);
        neighbours[6] = new Vector2Int(direction.x - range, direction.y + 0);
        neighbours[7] = new Vector2Int(direction.x - range, direction.y + range);

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

        Vector2Int[] threatened;

        if (type == AttackType.Melee)
        {
            threatened = new[]
            {
            neighbours[sectors[sector][positions[0]]],
            neighbours[sectors[sector][positions[1]]],
            neighbours[sectors[sector][positions[2]]]
            };
        }
        else//(type == Type.Ranged)
        {
            var attackRange = 3;
            threatened = new Vector2Int[attackRange];
            //RangedAttack
            for (int i = 0; i < attackRange; i++)
            {
                threatened[i] = neighbours[sectors[sector][positions[0]]] + (neighbours[sectors[sector][positions[0]]] - direction) * i;
            }
        }
        //else
        //{
        //    //threatened = new[]
        //    //{
        //    ////neighbours[[sector][positions[0]]],
        //    ////neighbours[[sector][positions[1]]],
        //    ////neighbours[sectors[sector][positions[2]]],
        //    ////neighbours[sectors[sector][positions[3]]],
        //    ////neighbours[sectors[sector][positions[4]]],
        //    ////neighbours[sectors[sector][positions[5]]],
        //    ////neighbours[sectors[sector][positions[6]]]
        //    //};
        //}


        return threatened;

    }

    
}

public class GridCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject specialPrefab;
    public GameObject Targetprefab;
    public new Camera camera;
    public float rotationSpeed;
    private GridLayer grid;
    private GridUnit gridUnit;

   // private List<GridNode> neighbors;

    private Transform target;
    private bool targeting;
    private BasicAttack meleeAttack;
    private BasicAttack rangedAttack;
    private BasicAttack specialAttack;
    private Vector2Int[] currentAttack;

    private float timer;
    private bool attacked;
    private int[] melee;
    private int[] ranged;
    private int[] special;

    public Transform Target => target;




    void Start()
    {
        gridUnit = GetComponent<GridUnit>();
        grid = gridUnit.grid;
        melee = new int[]
        {
            0,1,2
        };
        ranged = new int[]
        {
            1
        };
        special = new int[]
        {
            0,1,2,3,4,5,6,7
        };
    }

    void Update()
    {
        //Attacks
        //if (Input.GetKeyDown(KeyCode.LeftShift) && !attacked)
        //{
        //   specialAttack = new BasicAttack(gridUnit.position + grid.WorldToCell(transform.forward * 2), special, 1);
        //    currentAttack = specialAttack.doAttack(transform.eulerAngles.y, 1, AttackType.Special);
        //    drawAttack(currentAttack);
        //    attacked = true;
        //}
        if (Input.GetMouseButtonDown(0) && !attacked)
        {
            meleeAttack = new BasicAttack(gridUnit.position,melee, 0.5f);
            currentAttack = meleeAttack.doAttack(transform.eulerAngles.y,1, AttackType.Melee);
            drawAttack(currentAttack);
            attacked = true;
        }
        if (Input.GetMouseButtonDown(1) && !attacked)
        {
            rangedAttack = new BasicAttack(gridUnit.position,ranged, 1);
            currentAttack = rangedAttack.doAttack(transform.eulerAngles.y, 1, AttackType.Ranged);
            drawAttack(currentAttack);
            attacked = true;
        }

        if (attacked)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                foreach (GameObject g in trash)
                    Destroy(g);
                timer = 0;
                attacked = false;
            }
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
    List<GameObject> trash;
    void drawAttack(Vector2Int[] attack)
    {
        trash = new List<GameObject>();
        for (int i = 0; i < attack.Length; i++)
        {
            Vector2Int gridPosition = attack[i];
            if (gridPosition.x < grid.numCols && gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.y < grid.numRows)
            {
                GridNode node = grid.nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;
                trash.Add(Instantiate(meleePrefab, worldPosition, meleePrefab.transform.rotation, null));
                if (node.unit != null)
                {
                    Destroy(node.unit.gameObject);
                }
            }

        }

    }

    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(target.position, Vector3.one);
            Gizmos.DrawLine(transform.position, target.position);

        }
    }

    void rotateTowards(Vector3 direction)
    {
        var newRotation = Quaternion.LookRotation(-1 * (transform.position - direction), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }


 
}