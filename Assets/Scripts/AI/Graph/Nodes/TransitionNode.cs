using UnityEngine;

[CreateNodeMenu("AI/Transition/Static")]
public class TransitionNode : StateGraphNode
{
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;
    [SerializeField] private StateNode target;

    public StateNode Target => target;
    public StateAction Action => action;

    public bool IsTriggered(StateMachine fsm)
    {
        return decision.Test(fsm);
    }
}