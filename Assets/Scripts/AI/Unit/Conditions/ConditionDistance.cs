using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Distance", menuName = "FSM/Condition/Distance")]
public class ConditionDistance : UnitCondition
{
    public ConditionType type;
    public float value;
    
    public override bool Test(UnitController unit)
    {
        var  distance = Vector3.Distance(unit.transform.position, unit.target.position);
        switch (type)
        {
            case ConditionType.Minimum:
                return distance >= value;
            case ConditionType.Maximum:
                return distance <= value;
            case ConditionType.Exact:
                return Math.Abs(distance - value) < 0.01f;
            default:
                return false;
        }
    }
}
