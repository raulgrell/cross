using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM.Custom
{
    /// <summary>A node for checking if a blackboard variable is null.</summary>
    [Node.CreateNodeMenuAttribute("FSM/Check/IsTrue")]
    public class IsTrueCheck : ConditionNode
    {
        /// <summary>Initialize node for execution.</summary>
        protected override void Init()
        {
            _expectedType = typeof(BoolNode);
            base.Init();
        }

        /// <summary>Execute the check.</summary>
        protected override bool CheckCondition()
        {
            // Check if variable is null.
            return GetBlackboardValue<bool>("_variable", _variable);
        }
    }
}