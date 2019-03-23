﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType {
    Minimum,
    Maximum,
    Exact
}

public abstract class UnitCondition : StateCondition
{
    public override bool Test<T>(FiniteStateMachine<T> unit)
    {
        return Test(unit.Agent as UnitController);
    }

    public abstract bool Test(UnitController fsm);
}
