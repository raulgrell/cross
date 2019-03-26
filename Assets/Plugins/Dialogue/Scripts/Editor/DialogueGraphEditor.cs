using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor
{
    [CustomNodeGraphEditor(typeof(DialogueGraph))]
    public class DialogueGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(System.Type type)
        {
            return type.Namespace == "Dialogue" ? base.GetNodeMenuName(type).Replace("Dialogue/", "") : null;
        }
    }
}