using UnityEditor;
using UnityEngine;
using XNodeEditor;

[NodeGraphEditor.CustomNodeGraphEditorAttribute(typeof(StateGraph))]
public class StateGraphEditor : AIGraphEditor
{
    public override string GetNodeMenuName(System.Type type)
    {
        bool isValid =
            type.IsSubclassOf(typeof(StateGraphNode)) ||
            type.IsSubclassOf(typeof(ActionNode)) ||
            type.IsSubclassOf(typeof(VariableNode));

        return isValid ? base.GetNodeMenuName(type).Replace("FSM/", "") : null;
    }
}