using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Manhattan", menuName = "FSM/Condition/Manhattan")]
public class ConditionPath : UnitCondition
{
    public ConditionType type;
    public int value;
    
    public override bool Test(UnitController unit)
    {
        var dist = unit.Path.Length;
        
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
