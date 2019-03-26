using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;
using System;

[CustomEditor(typeof(Variable), true)]
public class GenericVariableEditor : Editor {

    bool _runtimeFoldout = true;

    public override void OnInspectorGUI() {
        // Draw super.
        base.OnInspectorGUI();
        // Only show on runtime.
        EditorGUILayout.Separator();
        _runtimeFoldout = EditorGUILayout.Foldout(_runtimeFoldout, "Runtime");
        if (_runtimeFoldout) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Value", target.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}