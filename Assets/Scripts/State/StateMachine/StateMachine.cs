using UnityEngine;
using XNode;

public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
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

public abstract class StateGraph : NodeGraph
{
    public abstract class Result
    {
    }

    public abstract void Init(Blackboard blackboard);

    public abstract Result Run(Blackboard blackboard);
}