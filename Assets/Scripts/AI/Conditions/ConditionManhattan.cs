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
    public bool negate;
    
    public override bool Test(UnitController unit)
    {
        var distVec = unit.unit.grid.WorldToCell(unit.target.position) - unit.unit.Position;
        var dist = Mathf.Abs(distVec.x) + Mathf.Abs(distVec.y);
        
        switch (type)
        {
            case ConditionType.Minimum:
                return dist >= value != negate;
            case ConditionType.Maximum:
                return dist <= value != negate;
            case ConditionType.Exact:
                return dist == value != negate;
            default:
                return negate;
        }
    }
}
