using System;
using QAI.FSM;
using UnityEngine;
using XNode;

[CreateNodeMenuAttribute("FSM/Condition/Shared")]
class GenericCondition : ConditionNode
{
    [SerializeField] private StateCondition condition;

    protected override void Init()
    {
        _expectedType = typeof(BoolNode);
        base.Init();
    }

    protected override bool CheckCondition()
    {
        throw new NotImplementedException();
    }
}