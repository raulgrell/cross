using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "FSM/Condition/Health")]
public class ConditionHealth : UnitCondition
{
    public ConditionType type;
    public float value;
    
    public override bool Test(UnitController unit)
    {
        switch (type)
        {
            case ConditionType.Minimum:
                return unit.health > value;
            case ConditionType.Maximum:
                return unit.health < value;
            case ConditionType.Exact:
                return unit.health == value;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
