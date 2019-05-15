using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Dialogue;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public abstract class UnitController : MonoBehaviour
{
    public Transform target;
    internal GridUnit unit;
    internal GridCombat combat;
    internal GridNode[] path;
    internal int nodeIndex;
    internal GridNode gridWaypoint;
    internal float actionTimer;
    
    [Range(0.01f, 1f)] public float refreshInterval = 0.2f;

    protected virtual void Start()
    {
        unit = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
    }

    public void GoToNextWaypoint()
    {
        if (path == null || path.Length == 0 || nodeIndex >= path.Length)
            return;

        gridWaypoint = path[nodeIndex];
        unit.MoveTowards(gridWaypoint.gridPosition);
        nodeIndex += 1;
    }

    public bool AtCurrentWaypoint()
    {
        if (unit.state == GridUnitState.Moving)
            return false;

        if (path == null || nodeIndex >= path.Length)
            return false;

        return true;
    }

    public void Attack()
    {
        combat.Attack(combat.meleeAttack);
    }

    public void MoveAwayFromTarget()
    {
        var targetPos = unit.grid.WorldToCell(target.position);
        var d = targetPos - unit.position;
    }

    protected IEnumerator RequestPath()
    {
        Vector3 targetPositionOld = target.position + Vector3.up;

        while (target)
        {
            var targetPos = target.position;
            
            if (targetPositionOld != targetPos)
            {
                targetPositionOld = target.position;
                path = GridPathfinding.RequestPath(transform.position, targetPos);
                nodeIndex = 0;
                if (path.Length > 0)
                    gridWaypoint = path[0];
            }

            if (refreshInterval != 0)
            {
                yield return new WaitForSeconds(refreshInterval);
            }
        }
    }

    public void LookAtTarget()
    {
        unit.LookAt(unit.grid.WorldToCell(target.position));
    }

    public void GrabTarget()
    {
        target.gameObject.GetComponent<GridUnit>().SetGrabbed();
    }

    public void ResetTimer(float seconds = 0)
    {
        actionTimer = seconds;
    }
}