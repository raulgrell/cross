using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Dialogue;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public abstract class UnitController : MonoBehaviour
{
    public Transform target;
    
    protected GridNode gridWaypoint;
    protected GridUnit unit;
    protected GridCombat combat;
    protected GridNode[] path;
    protected int nodeIndex;
    protected float actionTimer;
    protected bool needNewPath;

    public GridCombat Combat => combat;
    public GridUnit Unit => unit;
    public GridNode[] Path => path;
    
    public bool Ready => actionTimer < 0;

    [Range(0f, 1f)] public float refreshInterval = 0.5f;

    protected virtual void Start()
    {
        unit = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
    }

    private void Update()
    {
        actionTimer -= Time.deltaTime;
    }

    public void GoToNextWaypoint()
    {
        if (path == null || path.Length == 0 || nodeIndex >= path.Length)
            return;

        gridWaypoint = path[nodeIndex];
        nodeIndex += 1;

        needNewPath = !unit.MoveTowards(gridWaypoint.gridPosition);
    }

    public bool AtCurrentWaypoint()
    {
        if (unit.State == GridUnitState.Moving)
            return false;

        return path != null && nodeIndex <= path.Length;
    }

    public void Attack()
    {
        combat.Attack(combat.meleeAttack);
    }

    public void MoveAwayFromTarget()
    {
        var targetPos = unit.Grid.WorldToCell(target.position);
        var d = unit.Position - targetPos;
        unit.MoveTowards(d);
    }
    
    public void MoveTowardsTarget()
    {
        var targetPos = unit.Grid.WorldToCell(target.position);
        var d = targetPos - unit.Position;
        unit.MoveTowards(d);
    }

    protected IEnumerator RequestPath()
    {
        Vector3 targetPositionOld = target.position + Vector3.up;

        while (target)
        {
            var targetPos = target.position;
            
            if (needNewPath || targetPositionOld != targetPos)
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
        unit.LookAt(unit.Grid.WorldToCell(target.position));
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