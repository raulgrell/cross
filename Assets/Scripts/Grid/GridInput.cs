using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public enum TargetState
{
    None,
    Targeting,
    Locked
}

[RequireComponent(typeof(GridUnit))]
public class GridInput : MonoBehaviour
{
    private GridUnit unit;
    private GridCombat combat;
    public new Camera camera;

    private TargetState state;
    
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

        switch (state)
        {
            case TargetState.Targeting:
                if (Input.GetKeyUp(KeyCode.Tab))
                {
                    Time.timeScale = 1f;
                    if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
                    {
                        combat.Target = hitInfo.transform;
                        state = TargetState.Locked;
                    }
                    else
                    {
                        combat.Target = null;
                        state = TargetState.None;
                    }
                }
                break;
            case TargetState.Locked:
                Vector3 newTarget = combat.Target.position.SetY(transform.position.y);
                RotateTowards(newTarget);
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Time.timeScale = 0.2f;
                    state = TargetState.Targeting;
                }
                break;
            case TargetState.None:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Time.timeScale = 0.2f;
                    state = TargetState.Targeting;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void RotateTowards(Vector3 direction)
    {
        var newRotation = Quaternion.LookRotation(-1 * (transform.position - direction), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1f);
    }
}
