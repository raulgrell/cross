using System;
using UnityEngine;
using XNode;

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

public class StateGraphNode : Node
{
    protected StateGraph Graph => graph as StateGraph;
}