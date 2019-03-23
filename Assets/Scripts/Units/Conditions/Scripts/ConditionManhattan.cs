using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manhattan", menuName = "FSM/Condition/Manhattan")]
public class ConditionManhattan : StateCondition
{
    public override bool Test<T>(FiniteStateMachine<T> fsm)
    {
        throw new System.NotImplementedException();
    }
}
