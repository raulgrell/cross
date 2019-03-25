using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BasicAttack
{
    public Vector2Int[] positions;
    public float speed;

    public BasicAttack(Vector2Int[] pos, float speed)
    {
        positions = pos;
        this.speed = speed;
    }
}

public class GridCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject specialPrefab;
    public GameObject Target;
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
    private BasicAttack currentAttack;

    private float timer;
    private bool attacked;
    


    void Start()
    {
        gridUnit = GetComponent<GridUnit>();
        grid = gridUnit.grid;
    }

    void Update()
    {
        //Attacks
        DrawTarget(transform.eulerAngles.y, 1);
        

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

        }
    }

    void rotateTowards(Vector3 direction)
    {
        var newRotation = Quaternion.LookRotation(-1 * (transform.position - direction), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }


    void DrawTarget(float rotation, int range)
    {
        Vector2Int newPos = new Vector2Int();
        Vector2Int direction = gridUnit.position;
        
        // t, tr, r, br, b, bl, l, tl
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

        Vector2Int[] threatenedMelee;
        int attackRange = 3;
        Vector2Int[] threatenedRanged = new Vector2Int[attackRange];

        Vector2Int[] threatenedSpecial = new Vector2Int[attackRange];


        //meleeAttack
        threatenedMelee = new[]
        {
            neighbours[sectors[sector][0]],
            neighbours[sectors[sector][1]],
            neighbours[sectors[sector][2]]
        };

        //RangedAttack
        for (int i = 0; i < attackRange; i++)
        {
            threatenedRanged[i] = neighbours[sectors[sector][1]] + (neighbours[sectors[sector][1]] - direction) * i;
        }

       
       
        //Special Attaack in progress
        //else
        //{
        //    if ((direction.x - neighbours[sectors[sector][1]].x >= 1 || direction.x - neighbours[sectors[sector][1]].x <= -1) && direction.y - neighbours[sectors[sector][1]].y == 0)
        //    {
        //        threatened = new[]
        //        {
        //         new Vector2Int (neighbours[sectors[sector][1]].x + 1,neighbours[sectors[sector][1]].y ),
        //         //neighbours[sectors[sector][1]],
        //         //neighbours[sectors[sector][2]]
        //         };
        //    }
        //}

        meleeAttack = new BasicAttack(threatenedMelee,0.5f);
        rangedAttack = new BasicAttack(threatenedRanged,1);
        if (Input.GetMouseButtonDown(0) && !attacked)
        {
            doAttack(meleeAttack);
            currentAttack = meleeAttack;
            attacked = true;
        }
        if (Input.GetMouseButtonDown(1) && !attacked)
        {
            doAttack(rangedAttack);
            currentAttack = rangedAttack;
            attacked = true;
        }

        if (attacked)
        {
            timer += Time.deltaTime;
            
            if (timer > currentAttack.speed)
            {
                foreach (GameObject g in trash)
                    Destroy(g);
                timer = 0;
                attacked = false;
            }
        }
    }
    List<GameObject> trash;
    void doAttack(BasicAttack attack)
    {
        trash = new List<GameObject>();
        for (int i = 0; i < attack.positions.Length; i++)
        {
            Vector2Int gridPosition = attack.positions[i];
            Debug.Log(attack.positions[i]);
            if (gridPosition.x < grid.numCols && gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.y < grid.numRows)
            {
                GridNode node = grid.nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;
                trash.Add(Instantiate(meleePrefab,worldPosition,meleePrefab.transform.rotation, null));
                if (node.unit != null)
                {   
                Destroy(node.unit.gameObject);
                }
            }

        }
        
    }
}