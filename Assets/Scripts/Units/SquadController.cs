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
    public GridLayer grid;
    public UnitController leader;
    
    private UnitController target;
    private UnitController[] units;
    private SquadState state;
    
    public IEnumerable<UnitController> Units => units;

    List<GridNode> GetNodesAroundTarget()
    {
        if (!target) return null;
        var pos = target.Unit.Position;
        var targetNode = grid.Nodes[pos.y, pos.x];
        return grid.GetNeighbours(targetNode, 1);
    }

    private void Start()
    {
        state = SquadState.None;
        target = FindObjectOfType<PlayerController>();
        units = GetComponentsInChildren<UnitController>();
    }

    private void Update()
    {
        switch (units.Length)
        {
            case 0: state = SquadState.Defeated; break;
            case 1: state = SquadState.Retreating; break;
            case 2: state = SquadState.Advancing; break;
            default: state = SquadState.Flanking; break;
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        if (leader == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(leader.transform.position, 0.1f);
    }
}