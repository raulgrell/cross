using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType {
    Minimum,
    Maximum,
    Exact
}

public abstract class UnitCondition : StateCondition
{
    public override bool Test(StateMachine unit)
    {
        return Test(unit.Agent);
    }

    public abstract bool Test(UnitController fsm);
}
