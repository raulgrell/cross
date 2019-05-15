using UnityEngine;

[CreateNodeMenu("AI/Action/Static")]
public class ActionNode : StateGraphNode
{
    [SerializeField] private StateAction action;
}