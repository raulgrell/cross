﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class EnemyController : UnitController
{
    private EnemyBehaviour behaviour;

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
}