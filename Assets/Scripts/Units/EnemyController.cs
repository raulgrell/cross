using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class EnemyController : UnitController
{
    private EnemyBehaviour behaviour;
    private SquadController squad;

    protected override void Start()
    {
        base.Start();
        behaviour = GetComponent<EnemyBehaviour>();
        StartCoroutine(RequestPath());
    }

    private void OnValidate()
    {
        unit = GetComponent<GridUnit>();
    }

    private void OnDrawGizmos()
    {
        if (unit == null || unit.grid == null || gridWaypoint == null)
            return;

        var pos = unit.grid.CellToWorld(gridWaypoint.gridPosition);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, Vector3.one);
        
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
}