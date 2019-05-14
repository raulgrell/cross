using System;
using UnityEngine;

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
    private new PlayerAnimation animation;
    public new Camera camera;

    private TargetState state;

    void Start()
    {
        unit = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
        animation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        switch (combat.State)
        {
            case CombatState.Idle:
                if (Input.GetKey(KeyCode.K))
                    animation.Dance();
                else if (Input.anyKey)
                    animation.StopDancing();

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    combat.Block();
                    animation.BlockAnimation();
                    if (Input.GetMouseButtonDown(1)) combat.Attack(combat.specialAttack);
                }
                else 
                {
                    if (Input.GetMouseButtonDown(0)) combat.Attack(combat.meleeAttack);
                    if (Input.GetMouseButtonDown(1)) combat.Attack(combat.rangedAttack);
                }

                if (Input.GetKeyDown(KeyCode.A)) unit.Move(Vector2Int.left);
                if (Input.GetKeyDown(KeyCode.D)) unit.Move(Vector2Int.right);
                if (Input.GetKeyDown(KeyCode.S)) unit.Move(Vector2Int.down);
                if (Input.GetKeyDown(KeyCode.W)) unit.Move(Vector2Int.up);
                break;
            case CombatState.Block:
                break;
            case CombatState.Threat:
            case CombatState.Attack:
                break;
            case CombatState.Hurt:
                break;
            case CombatState.Interacting:
                if (Input.GetKeyDown(KeyCode.A)) unit.Move(Vector2Int.left);
                if (Input.GetKeyDown(KeyCode.D)) unit.Move(Vector2Int.right);
                if (Input.GetKeyDown(KeyCode.S)) unit.Move(Vector2Int.down);
                if (Input.GetKeyDown(KeyCode.W)) unit.Move(Vector2Int.up);
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