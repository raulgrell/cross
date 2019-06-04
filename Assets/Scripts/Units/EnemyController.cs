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
        if (unit == null || unit.Grid == null || gridWaypoint == null)
            return;

        var pos = unit.Grid.CellToWorld(gridWaypoint.gridPosition);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, Vector3.one);
        
        if (Path == null)
            return;
        
        for (int i = nodeIndex; i < Path.Length; i++)
        {
            Vector3 lineStart = i == nodeIndex
                ? transform.position : Path[i - 1].transform.position + Vector3.up;
            Vector3 lineEnd = Path[i].transform.position + Vector3.up;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(lineEnd, 0.2f);
            Gizmos.DrawLine(lineStart, lineEnd);
        }
    }
}