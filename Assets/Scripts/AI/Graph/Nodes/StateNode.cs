using System.Collections.Generic;
using UnityEngine;

[CreateNodeMenu("AI/State/Static")]
public class StateNode : StateGraphNode
{
    [SerializeField] private StateAction entryAction;
    [SerializeField] private StateAction exitAction;
    [SerializeField] private StateAction[] actions;

    [SerializeField]
    private List<TransitionNode> transitions;

    public StateAction EntryAction => entryAction;
    public StateAction ExitAction => exitAction;
    public StateAction[] Actions => actions;
    public TransitionNode[] Transitions => transitions.ToArray();

    public virtual void Run()
    {
        Debug.Log("Default");
    }
    
    public void Trigger()
    {
        (graph as StateGraph).fsm.CurrentState.value = this;
    }
}