using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "FSM/Action/Attack")]
public class ActionAttack : StateAction 
{
    public override void Act<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
