using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "FSM/Condition/Health")]
public class ConditionHealth : UnitCondition
{
    public ConditionType type;
    public int value;
    
    public override bool Test(UnitController unit)
    {
        var health = unit.GetComponent<CombatHealth>().health;
        
        switch (type)
        {
            case ConditionType.Minimum:
                return health >= value;
            case ConditionType.Maximum:
                return health <= value;
            case ConditionType.Exact:
                return health == value;
            default:
                return false;
        }
    }
}
