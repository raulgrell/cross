using System;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState
{
    Idle,
    Block,
    Threat,
    Attack,
}

public class GridCombat : MonoBehaviour
{
    public UnitAttack meleeAttack;
    public UnitAttack rangedAttack;
    public UnitAttack specialAttack;

    private CombatState state;
    private Transform target;
    private GridLayer grid;
    private GridUnit unit;
    private bool targeting;

    private List<GameObject> trash;

    private float timer;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public CombatState State => state;

    void Start()
    {
        unit = GetComponent<GridUnit>();
        grid = unit.grid;
        state = CombatState.Idle;
    }

    void Update()
    {

        switch (state)
        {
            case CombatState.Idle:
                break;
            case CombatState.Block:
                break;
            case CombatState.Threat:
            case CombatState.Attack:
                timer += Time.deltaTime;

                if (timer > 0.5f)
                {
                    timer = 0;
                    state = CombatState.Idle;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Attack(UnitAttack attack)
    {
        state = CombatState.Attack;
        timer = 0;
        
        var threatened = attack.GetThreatened(unit);

        for (int i = 0; i < threatened.Length; i++)
        {
            Vector2Int gridPosition = threatened[i].position;
            if (grid.InBounds(gridPosition.x, gridPosition.y))
            {
                GridNode node = grid.nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;

                Instantiate(meleeAttack.attackPrefab, worldPosition, meleeAttack.attackPrefab.transform.rotation, null);

                if (node.unit != null)
                {
                    node.unit.gameObject.GetComponent<CombatHealth>().Damage(1);
                    if (node.unit.gameObject.GetComponent<CombatHealth>().health <= 0)
                    {
                        Destroy(gameObject);
                    }

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!target) return;
        //Gizmos.matrix = transform.worldToLocalMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(target.position, Vector3.one);
        Gizmos.DrawLine(transform.position, target.position);
    }
}