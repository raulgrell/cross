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
    public bool negate;
    
    public override bool Test(UnitController unit)
    {
        var dist = unit.path.Length;
        
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
