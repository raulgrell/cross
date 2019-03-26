using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitAction : StateAction
{
    public override void Act<T>(StateMachine<T> fsm)
    {
        Act(fsm.Agent as UnitController);
    }

    public abstract void Act(UnitController agent);
}
