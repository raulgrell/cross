using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Manhattan", menuName = "FSM/Condition/Manhattan")]
public class ConditionManhattan : UnitCondition
{
    public ConditionType type;
    public int value;
    
    public override bool Test(UnitController unit)
    {
        var distVec = unit.Unit.Grid.WorldToCell(unit.target.position) - unit.Unit.Position;
        var dist = Mathf.Abs(distVec.x) + Mathf.Abs(distVec.y);
        
        switch (type)
        {
            case ConditionType.Minimum:
                return dist >= value;
            case ConditionType.Maximum:
                return dist <= value;
            case ConditionType.Exact:
                return dist == value;
            default:
                return false;
        }
    }
}
