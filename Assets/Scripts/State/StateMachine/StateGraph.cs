using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class StateGraph : NodeGraph
{
    public abstract void Init();
    public abstract bool Run();
}

[NodeWidth(360)]
public abstract class StateGraphNode : Node
{
    protected StateGraph Graph => graph as StateGraph;
}

[CreateAssetMenu(fileName = "StateGraph", menuName = "AI/StateGraph", order = 1)]
public class UnitGraph : StateGraph
{
    public override void Init()
    {
    }

    public override bool Run()
    {
        return false;
    }
}


[CreateNodeMenu("AI/State/Root")]
public class RootStateNode : StateGraphNode
{
    [SerializeField] private State state;
}

[CreateNodeMenu("AI/State/Static")]
public class StateNode : StateGraphNode
{
    [SerializeField] private StateAction entryAction;
    [SerializeField] private StateAction exitAction;
    [SerializeField] private StateAction[] actions;

    [SerializeField]
    private List<StateGraphTransitionNode> transitions;

    public StateAction EntryAction => entryAction;
    public StateAction ExitAction => exitAction;
    public StateAction[] Actions => actions;
    public StateGraphTransitionNode[] Transitions => transitions.ToArray();
}

[CreateNodeMenu("AI/Transition/Static")]
public class StateGraphTransitionNode : StateGraphNode
{
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;
    [SerializeField] private StateNode target;

    public StateNode Target => target;
    public StateAction Action => action;

    public bool IsTriggered<T>(StateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}

[CreateNodeMenu("AI/Action/Static")]
public class ActionNode : StateGraphNode
{
    [SerializeField] private StateAction action;
}

[CreateNodeMenu("AI/State/Subgraph")]
public class StateSubGraph : StateGraphNode
{
    [Input] public bool exec;
    public NodeGraph subGraph;
    [Output] public bool output;
}