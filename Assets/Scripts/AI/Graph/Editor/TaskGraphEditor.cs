using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomNodeGraphEditorAttribute(typeof(TaskGraph))]
public class TaskGraphEditor : AIGraphEditor
{
    public override string GetNodeMenuName(System.Type type)
    {
        bool isValid =
            type.IsSubclassOf(typeof(TaskNode));

        return isValid ? base.GetNodeMenuName(type).Replace("FSM/", "") : null;
    }
}