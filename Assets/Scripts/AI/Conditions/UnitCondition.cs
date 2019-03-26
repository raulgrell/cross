using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public enum ConditionType {
    Minimum,
    Maximum,
    Exact
}

public abstract class UnitCondition : StateCondition
{
    public override bool Test<T>(StateMachine<T> unit)
    {
        return Test(unit.Agent as UnitController);
    }

    public abstract bool Test(UnitController fsm);
}
