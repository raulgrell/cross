using System;
using UnityEngine;
using UnityEditor;
     
[CustomPropertyDrawer(typeof(Variable), true)]
public class VariableDrawer : PropertyDrawer
{
    Editor editor;
     
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        if (property.objectReferenceValue != null)
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
     
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            GUILayout.BeginVertical("box");
     
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
     
            EditorGUI.BeginChangeCheck();

            if (editor)
                editor.OnInspectorGUI ();
            
            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
     
            GUILayout.EndVertical ();
            EditorGUI.indentLevel--;
        }
    }
}
     
[CanEditMultipleObjects]
[CustomEditor(typeof(UnityEngine.Object), true)]
public class UnityObjectEditor : Editor
{
}
