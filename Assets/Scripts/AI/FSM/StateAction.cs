using UnityEngine;

public abstract class StateAction : ScriptableObject
{
    public abstract bool Act(StateMachine fsm);
}