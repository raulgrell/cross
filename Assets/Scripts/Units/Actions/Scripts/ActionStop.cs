using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stop", menuName = "FSM/Action/Stop")]
public class ActionStop : StateAction 
{
    public override void Act<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
