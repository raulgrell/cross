using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquadState
{
    None,
    Flanking,
    Special
}

public enum BattleState
{
    Advancing,
    Retreating
}

public class SquadController : MonoBehaviour
{
    public GridLayer grid;
    public GridUnit leader;
    public List<GridUnit> units;
}