using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class EnemyController : UnitController
{
    [Range(0.01f, 1f)] public float refreshInterval = 0.2f;

    private EnemyBehaviour behaviour;

    void Start()
    {
        unit = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
        behaviour = GetComponent<EnemyBehaviour>();
        StartCoroutine(RequestPath());
    }

    private void OnValidate()
    {
        unit = GetComponent<GridUnit>();
    }

    private void OnDrawGizmos()
    {
        if (unit == null || unit.grid == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(unit.grid.CellToWorld(gridWaypoint), Vector3.one);
        
        if (path == null)
            return;
        
        for (int i = nodeIndex; i < path.Length; i++)
        {
            Vector3 lineStart = i == nodeIndex
                ? transform.position : path[i - 1].transform.position + Vector3.up;
            Vector3 lineEnd = path[i].transform.position + Vector3.up;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(lineEnd, 0.2f);
            Gizmos.DrawLine(lineStart, lineEnd);
        }
    }

    IEnumerator RequestPath()
    {
        Vector3 targetPositionOld = target.position + Vector3.up;

        while (true)
        {
            var targetPos = target.position;
            
            if (targetPositionOld != targetPos)
            {
                targetPositionOld = target.position;
                path = GridPathfinding.RequestPath(transform.position, targetPos);
                nodeIndex = 0;
                if (path.Length > 0)
                    gridWaypoint = path[0].gridPosition;
            }

            if (refreshInterval != 0)
            {
                yield return new WaitForSeconds(refreshInterval);
            }
            else
            {
                yield break;
            }
        }
    }
}