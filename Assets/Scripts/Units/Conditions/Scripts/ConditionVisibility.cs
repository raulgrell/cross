using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Visibility", menuName = "FSM/Condition/Visibility")]
public class ConditionVisibility : StateCondition
{
    public override bool Test<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
