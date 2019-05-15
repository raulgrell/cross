using System;
using UnityEngine;

[Serializable]
public class StateTransition
{
    [SerializeField] private StateVariable target;
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;

    public StateVariable Target => target;
    public StateAction Action => action;

    public bool IsTriggered(StateMachine fsm)
    {
        return decision.Test(fsm);
    }
}