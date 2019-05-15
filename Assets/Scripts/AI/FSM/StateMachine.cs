using UnityEngine;

public abstract class StateMachine : MonoBehaviour 
{
    public World world;
    public StateGraph graph;
    public StateVariable initialState;
    private StateVariable currentState;
    private UnitController agent;
    
    public StateVariable CurrentState => currentState;
    public UnitController Agent => agent;

    private void Start()
    {
        currentState = initialState;
        agent = GetComponent<UnitController>();
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