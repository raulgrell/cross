using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stop", menuName = "FSM/Action/Stop")]
public abstract class UnitAction : StateAction
{
    public override void Act<T>(FiniteStateMachine<T> fsm)
    {
        Act(fsm.Agent as UnitController);
    }

    public abstract void Act(UnitController agent);
}
