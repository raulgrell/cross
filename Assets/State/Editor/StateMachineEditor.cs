using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace QAI.FSM
{
    [CustomNodeGraphEditor(typeof(StateMachineGraph))]
    public class StateMachineEditor : AIGraphEditor
    {
        public override string GetNodeMenuName(System.Type type)
        {
            bool isValid =
                type.IsSubclassOf(typeof(StateNode)) ||
                type.IsSubclassOf(typeof(ActionNode)) ||
                type.IsSubclassOf(typeof(VariableNode)) ||
                type.IsSubclassOf(typeof(ConditionNode));

            return isValid ? base.GetNodeMenuName(type).Replace("FSM/", "") : null;
        }
    }
}