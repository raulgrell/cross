using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM.Custom
{
    /// <summary>A node for checking if a blackboard variable is null.</summary>
    [Node.CreateNodeMenuAttribute("FSM/Check/IsNull")]
    public class IsNullCheck : ConditionNode
    {
        /// <summary>Execute the check.</summary>
        protected override bool CheckCondition()
        {
            // Check if variable is null.
            object variable = GetBlackboardValue<object>("_variable", _variable);
            if (variable == null)
                return true;
            
            // Check if variable is of value type.
            System.Type type = variable.GetType();
            return type.IsValueType && variable == System.Activator.CreateInstance(type);
        }
    }
}