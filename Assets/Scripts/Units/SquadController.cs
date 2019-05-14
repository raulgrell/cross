using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GridLayer grid;
    public GridUnit leader;
    public List<GridUnit> units;
    private SquadState state;

    Vector2Int[] GetNodesAroundTarget()
    {
        return new Vector2Int[]{};
    }

    private void Start()
    {
        state = SquadState.None;
        
    }
}