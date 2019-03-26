using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;
using System;

[CustomPropertyDrawer(typeof(Reference), true)]
public class ReferenceDrawer : PropertyDrawer
{
    private GUIStyle popupStyle;
    private GUIStyle buttonStyle;
    private bool isConstant;

    private const string AssetsBase = "Assets";
    private static readonly string[] options = {"Use Constant", "Use Variable"};

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (popupStyle == null)
        {
            popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
            {
                imagePosition = ImagePosition.ImageOnly
            };
        }

        if (buttonStyle == null)
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();
        SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        SerializedProperty variable = property.FindPropertyRelative("Variable");

        // Calculate rect for configuration button
        Rect buttonRect = new Rect(position);
        buttonRect.yMin += popupStyle.margin.top;
        buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        isConstant = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, options, popupStyle) == 0;
        useConstant.boolValue = isConstant;
        if (!isConstant)
        {
            Rect rect = new Rect(position) {xMin = position.xMax - 100};
            position.xMax = rect.xMin - buttonStyle.margin.left;

            if (GUI.Button(rect, "Create New"))
                variable.objectReferenceValue = CreateNew(variable);
        }

        EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * (isConstant ? 2 : 1);
    }

    public static Type GetType(SerializedProperty property)
    {
        string[] parts = property.propertyPath.Split('.');
        Type currentType = property.serializedObject.targetObject.GetType();

        for (int i = 0; i < parts.Length; i++)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy |
                               BindingFlags.Instance;
            currentType = currentType.GetField(parts[i], bindingFlags).FieldType;
        }

        Type targetType = currentType;
        return targetType;
    }

    public ScriptableObject CreateNew(SerializedProperty property)
    {
        var type = GetType(property);
        var obj = ScriptableObject.CreateInstance(type);

        if (!Directory.Exists(AssetsBase))
        {
            Directory.CreateDirectory(AssetsBase);
            AssetDatabase.Refresh();
        }

        string dest = EditorUtility.SaveFilePanel("Save object as", AssetsBase, type.Name, "asset");
        if (!string.IsNullOrEmpty(dest) && TryParseAssetPath(dest, out dest))
        {
            AssetDatabase.CreateAsset(obj, dest);
            AssetDatabase.Refresh();
            return obj;
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(obj);
            return null;
        }
    }

    private bool TryParseAssetPath(string path, out string dest)
    {
        var commonRoot = Path.Combine(Application.dataPath, path);
        if (commonRoot.StartsWith(Application.dataPath))
        {
            dest = AssetsBase + path.Substring(Application.dataPath.Length);
            return true;
        }

        dest = path;
        return false;
    }
}

[CustomPropertyDrawer(typeof(BoolReference))]
public class BoolReferenceDrawer : ReferenceDrawer
{ }

