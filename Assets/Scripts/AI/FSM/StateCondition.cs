using UnityEngine;

public abstract class StateCondition : ScriptableObject
{
    public abstract bool Test(StateMachine fsm);
}