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
    Special
}

public class SquadController : MonoBehaviour
{
    public GridUnit target;
    public GridLayer grid;
    public GridUnit leader;
    public List<GridUnit> units;
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

    private void OnDrawGizmosSelected()
    {
        foreach (GridUnit gridUnit in units)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(gridUnit.transform.position, 0.1f);
        }
    }
}