using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(StateGraph))]
public class StateGraphEditor : NodeGraphEditor
{
    /// <summary> 
    /// Overriding GetNodeMenuName lets you control if and how nodes are categorized.
    /// </summary>
    public override string GetNodeMenuName(System.Type type)
    {
        return "Node";
    }
}