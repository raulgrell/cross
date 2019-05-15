using UnityEditor;
using UnityEngine;
using XNodeEditor;

public abstract class AIGraphEditor : NodeGraphEditor
{
    public override void OnGUI()
    {
        if (Event.current.type == EventType.Repaint)
            NodeEditorWindow.current.Repaint();
    }
}
