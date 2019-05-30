using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum SquadState
{
    None,
    Flanking,
    Advancing,
    Retreating,
    Celebrating,
    Defeated
}

public class SquadController : MonoBehaviour
{
    public GridUnit target;
    public GridLayer grid;
    public UnitController leader;
    public List<UnitController> units;
    private SquadState state;

    List<GridNode> GetNodesAroundTarget()
    {
        if (!target) return null;
        var pos = target.Position;
        var targetNode = grid.Nodes[pos.y, pos.x];
        return grid.GetNeighbours(targetNode, 1);
    }

    private void Start()
    {
        state = SquadState.None;
    }

    private void Update()
    {
        switch (units.Count)
        {
            case 0: state = SquadState.Defeated; break;
            case 1: state = SquadState.Retreating; break;
            case 2: state = SquadState.Advancing; break;
            default: state = SquadState.Flanking; break;
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        if (units == null) return;
        
        foreach (var unit in units)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(unit.transform.position, 0.1f);
        }
    }
}