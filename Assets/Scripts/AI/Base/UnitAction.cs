using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : StateAction
{
    public event Action OnComplete;
    
    public override void Act<T>(StateMachine<T> fsm)
    {
        Act(fsm.Agent as UnitController);
    }

    public abstract void Act(UnitController agent);
}
