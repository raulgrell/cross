using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TargetFrame))]
public class TargetFrameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Clear indent
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var positionRect = new Rect(position.x, position.y, position.width / 2, position.height);
        var frameRect = new Rect(position.x + position.width / 2 + 5, position.y, position.width / 2, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("position"), GUIContent.none);
        EditorGUI.PropertyField(frameRect, property.FindPropertyRelative("frame"), GUIContent.none);

        // Reset indent
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}