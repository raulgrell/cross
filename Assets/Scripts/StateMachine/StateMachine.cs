using System;
using UnityEngine;

[Serializable]
public class State
{
    [SerializeField] private StateAction entryAction;
    [SerializeField] private StateAction exitAction;
    [SerializeField] private StateAction[] actions;
    [SerializeField] private StateTransition[] transitions;

    public StateAction EntryAction => entryAction;
    public StateAction ExitAction => exitAction;
    public StateAction[] Actions => actions;
    public StateTransition[] Transitions => transitions;

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }
}

[Serializable]
public class StateTransition
{
    [SerializeField] private StateVariable target;
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;

    public StateVariable Target => target;
    public StateAction Action => action;

    public bool IsTriggered<T>(StateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}

public abstract class StateAction : ScriptableObject
{
    public abstract void Act<T>(StateMachine<T> fsm) where T : MonoBehaviour;
}

public abstract class StateCondition : ScriptableObject
{
    public abstract bool Test<T>(StateMachine<T> fsm) where T : MonoBehaviour;
}

public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    public StateGraph graph;
    public StateVariable initialState;
    private StateVariable currentState;
    
    private T agent;

    public T Agent => agent;

    private void Start()
    {
        currentState = initialState;
        agent = GetComponent<T>();
    }

    private void Update()
    {
        StateTransition triggered = null;

        foreach (var transition in currentState.Value.Transitions)
        {
            if (!transition.IsTriggered(this)) continue;
            triggered = transition;
            break;
        }

        foreach (var action in currentState.Value.Actions)
            action.Act(this);

        if (triggered == null) return;

        if (currentState.Value.ExitAction)
            currentState.Value.ExitAction.Act(this);
        currentState.Value.OnExit();

        triggered.Target.Value.OnEnter();
        if (triggered.Target.Value.EntryAction)
            triggered.Target.Value.EntryAction.Act(this);

        if (triggered.Action)
            triggered.Action.Act(this);

        currentState = triggered.Target;
    }
}