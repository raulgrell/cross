using System;
using QAI.FSM;
using UnityEngine;
using XNode;

namespace Unit
{

[Node.CreateNodeMenuAttribute("FSM/Condition/Shared")]
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
            throw new System.NotImplementedException();
        }
    }
}
