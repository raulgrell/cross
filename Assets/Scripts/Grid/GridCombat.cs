using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatState
{
    Idle,
    Block,
    Parry,
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

    private float stateTimer;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public CombatState State
    {
        get => state;
        set
        {
            stateTimer = 0;
            state = value;
        }
    }

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
                if (stateTimer > 0.1f)
                    State = CombatState.Idle;
                break;
            case CombatState.Parry:
                if (stateTimer > 0.2f)
                    State = CombatState.Idle;
                break;
            case CombatState.Threat:
                if (stateTimer > 0.3f)
                    State = CombatState.Idle;
                break;
            case CombatState.Attack:
                if (stateTimer > 0.5f)
                    State = CombatState.Idle;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        stateTimer += Time.deltaTime;
    }

    public void Attack(UnitAttack attack)
    {
        state = CombatState.Attack;
        stateTimer = 0;
        
        var threatened = attack.GetThreatened(unit);

        for (int i = 0; i < threatened.Length; i++)
        {
            Target hit = threatened[i];
            Vector2Int gridPosition = hit.position;
            
            if (grid.InBounds(gridPosition.x, gridPosition.y))
            {
                GridNode node = grid.nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;

                Instantiate(meleeAttack.attackPrefab, worldPosition, meleeAttack.attackPrefab.transform.rotation, null);

                if (node.unit != null)
                {
                    if (node.unit.gameObject.GetComponent<CombatHealth>().Damage(1))
                    {
                        if(node.unit.transform.CompareTag("Player"))
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                        Destroy(node.unit.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!target) return;
        var targetPos = target.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(targetPos, Vector3.one);
        Gizmos.DrawLine(transform.position, targetPos);
    }

    public void Block()
    {
        State = CombatState.Block;
    }

    public void Parry()
    {
        State = CombatState.Parry;
    }
}