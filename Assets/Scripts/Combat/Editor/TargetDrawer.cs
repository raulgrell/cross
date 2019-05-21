using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Target))]
public class TargetDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Clear indent
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var positionRect = new Rect(position.x, position.y, position.width / 4 - 5, position.height);
        var effectRect = new Rect(position.x + position.width / 4 + 5, position.y, position.width / 4 - 5, position.height);
        var damageRect = new Rect(position.x + 2 * position.width / 4 + 10, position.y, position.width / 4 - 10, position.height);
        var knockbackRect = new Rect(position.x + 3 * position.width / 4 + 10, position.y, position.width / 4 - 10, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("position"), GUIContent.none);
        EditorGUI.PropertyField(effectRect, property.FindPropertyRelative("effect"), GUIContent.none);
        EditorGUI.PropertyField(damageRect, property.FindPropertyRelative("damage"), GUIContent.none);
        EditorGUI.PropertyField(knockbackRect, property.FindPropertyRelative("knockback"), GUIContent.none);

        // Reset indent
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}