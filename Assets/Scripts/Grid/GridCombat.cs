﻿using System;
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
                    foreach (GameObject g in trash)
                        Destroy(g);

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
        var threatened = attack.GetThreatened(unit);

        trash = new List<GameObject>();
        for (int i = 0; i < threatened.Length; i++)
        {
            Vector2Int gridPosition = threatened[i];
            if (grid.InBounds(gridPosition.x, gridPosition.y))
            {
                GridNode node = grid.nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;

                trash.Add(Instantiate(meleeAttack.attackPrefab, worldPosition, meleeAttack.attackPrefab.transform.rotation, null));

                if (node.unit != null)
                {

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