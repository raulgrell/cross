using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

[CreateAssetMenu(fileName = "Manhattan", menuName = "FSM/Condition/Manhattan")]
public class ConditionManhattan : UnitCondition
{
    public ConditionType type;
    public int value;
    public bool negate;
    
    public override bool Test(UnitController unit)
    {
        var dist = unit.unit.position - unit.unit.grid.WorldToNode(unit.target.position).gridPosition;
        dist.x = Mathf.Abs(dist.x);
        dist.y = Mathf.Abs(dist.y);
        
        switch (type)
        {
            case ConditionType.Minimum:
                return dist.x + dist.y >= value != negate;
            case ConditionType.Maximum:
                return dist.x + dist.y <= value != negate;
            case ConditionType.Exact:
                return dist.x + dist.y == value != negate;
            default:
                return negate;
        }
    }
}
