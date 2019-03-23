using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manhattan", menuName = "FSM/Condition/Manhattan")]
public class ConditionManhattan : UnitCondition
{
    public ConditionType type;
    public int value;
    
    public override bool Test(UnitController unit)
    {
        var dist = unit.unit.position - unit.unit.grid.WorldToNode(unit.target.position).gridPosition;
        switch (type)
        {
            case ConditionType.Minimum:
                return dist.x + dist.y > value;
            case ConditionType.Maximum:
                return dist.x + dist.y < value;
            case ConditionType.Exact:
                return dist.x + dist.y == value;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
