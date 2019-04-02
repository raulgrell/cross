using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class GridInput : MonoBehaviour
{
    private GridUnit unit;
    private GridCombat combat;
    
    void Start()
    {
        unit = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) unit.Move(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.D)) unit.Move(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.S)) unit.Move(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.W)) unit.Move(Vector2Int.up);

        switch (combat.State)
        {
            case CombatState.Idle:
                if (Input.GetMouseButtonDown(0)) combat.Attack(combat.meleeAttack);
                if (Input.GetMouseButtonDown(1)) combat.Attack(combat.rangedAttack);
                break;
            case CombatState.Block:
                break;
            case CombatState.Threat:
            case CombatState.Attack:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        

    }
}
