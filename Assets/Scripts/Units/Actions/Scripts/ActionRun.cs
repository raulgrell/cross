using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "FSM/Action/Run")]
public class ActionRun: StateAction 
{
    public override void Act<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
