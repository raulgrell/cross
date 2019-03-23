using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "FSM/Condition/Health")]
public class ConditionHealth : StateCondition
{
    public override bool Test<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
