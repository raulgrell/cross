using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
/// area that allows for changing the values on the object without having to change editor.
/// </summary>
public class ExpandableAttribute : PropertyAttribute
{
    public ExpandableAttribute()
    {
    }
}

/// <summary>
/// Draws the property field for any field marked with ExpandableAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
public class ExpandableAttributeDrawer : PropertyDrawer
{
    // Use the following area to change the style of the expandable ScriptableObject drawers;

    #region Style Setup

    private enum BackgroundStyles
    {
        None,
        HelpBox,
        Darken,
        Lighten
    }

    /// <summary>
    /// Whether the default editor Script field should be shown.
    /// </summary>
    private static bool SHOW_SCRIPT_FIELD = false;

    /// <summary>
    /// The spacing on the inside of the background rect.
    /// </summary>
    private static float INNER_SPACING = 6.0f;

    /// <summary>
    /// The spacing on the outside of the background rect.
    /// </summary>
    private static float OUTER_SPACING = 4.0f;

    /// <summary>
    /// The style the background uses.
    /// </summary>
    private static BackgroundStyles BACKGROUND_STYLE = BackgroundStyles.HelpBox;

    /// <summary>
    /// The colour that is used to darken the background.
    /// </summary>
    private static Color DARKEN_COLOUR = new Color(0.0f, 0.0f, 0.0f, 0.2f);

    /// <summary>
    /// The colour that is used to lighten the background.
    /// </summary>
    private static Color LIGHTEN_COLOUR = new Color(1.0f, 1.0f, 1.0f, 0.2f);

    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = 0.0f;

        totalHeight += EditorGUIUtility.singleLineHeight;

        if (property.objectReferenceValue == null)
            return totalHeight;

        if (!property.isExpanded)
            return totalHeight;

        SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

        if (targetObject == null)
            return totalHeight;

        SerializedProperty field = targetObject.GetIterator();

        field.NextVisible(true);

        if (SHOW_SCRIPT_FIELD)
        {
            totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        while (field.NextVisible(false))
        {
            totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        totalHeight += INNER_SPACING * 2;
        totalHeight += OUTER_SPACING * 2;

        return totalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect fieldRect = new Rect(position);
        fieldRect.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(fieldRect, property, label, true);

        if (property.objectReferenceValue == null)
            return;

        property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

        if (!property.isExpanded)
            return;

        SerializedObject targetObject = new SerializedObject(property.objectReferenceValue);

        if (targetObject == null)
            return;


        #region Format Field Rects

        List<Rect> propertyRects = new List<Rect>();
        Rect marchingRect = new Rect(fieldRect);

        Rect bodyRect = new Rect(fieldRect);
        bodyRect.xMin += EditorGUI.indentLevel * 14;
        bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                           + OUTER_SPACING;

        SerializedProperty field = targetObject.GetIterator();
        field.NextVisible(true);

        marchingRect.y += INNER_SPACING + OUTER_SPACING;

        if (SHOW_SCRIPT_FIELD)
        {
            propertyRects.Add(marchingRect);
            marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        while (field.NextVisible(false))
        {
            marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
            marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
            propertyRects.Add(marchingRect);
        }

        marchingRect.y += INNER_SPACING;

        bodyRect.yMax = marchingRect.yMax;

        #endregion

        DrawBackground(bodyRect);

        #region Draw Fields

        EditorGUI.indentLevel++;

        int index = 0;
        field = targetObject.GetIterator();
        field.NextVisible(true);

        if (SHOW_SCRIPT_FIELD)
        {
            //Show the disabled script field
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(propertyRects[index], field, true);
            EditorGUI.EndDisabledGroup();
            index++;
        }

        //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
        while (field.NextVisible(false))
        {
            try
            {
                EditorGUI.PropertyField(propertyRects[index], field, true);
            }
            catch (StackOverflowException)
            {
                field.objectReferenceValue = null;
                Debug.LogError("Detected self-nesting cauisng a StackOverflowException, avoid using the same " +
                               "object iside a nested structure.");
            }

            index++;
        }

        targetObject.ApplyModifiedProperties();

        EditorGUI.indentLevel--;

        #endregion
    }

    /// <summary>
    /// Draws the Background
    /// </summary>
    /// <param name="rect">The Rect where the background is drawn.</param>
    private void DrawBackground(Rect rect)
    {
        switch (BACKGROUND_STYLE)
        {
            case BackgroundStyles.HelpBox:
                EditorGUI.HelpBox(rect, "", MessageType.None);
                break;

            case BackgroundStyles.Darken:
                EditorGUI.DrawRect(rect, DARKEN_COLOUR);
                break;

            case BackgroundStyles.Lighten:
                EditorGUI.DrawRect(rect, LIGHTEN_COLOUR);
                break;
        }
    }
}

[CustomPropertyDrawer(typeof(ScriptableVariable), true)]
public class ScriptableVariableDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalHeight = EditorGUIUtility.singleLineHeight;
        if (property.isExpanded)
        {
            var data = property.objectReferenceValue as ScriptableObject;
            if (data == null) return EditorGUIUtility.singleLineHeight;
            SerializedObject serializedObject = new SerializedObject(data);
            SerializedProperty prop = serializedObject.GetIterator();
            if (prop.NextVisible(true))
            {
                do
                {
                    if (prop.name == "m_Script") continue;
                    var subProp = serializedObject.FindProperty(prop.name);
                    float height = EditorGUI.GetPropertyHeight(subProp, null, true) +
                                   EditorGUIUtility.standardVerticalSpacing;
                    totalHeight += height;
                } while (prop.NextVisible(false));
            }

            // Add a tiny bit of height if open for the background
            totalHeight += EditorGUIUtility.standardVerticalSpacing;
        }

        return totalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        if (property.objectReferenceValue != null)
        {
            property.isExpanded =
                EditorGUI.Foldout(
                    new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                    property.isExpanded, property.displayName, true);
            EditorGUI.PropertyField(
                new Rect(EditorGUIUtility.labelWidth + 14, position.y, position.width - EditorGUIUtility.labelWidth,
                    EditorGUIUtility.singleLineHeight), property, GUIContent.none, true);
            if (GUI.changed) property.serializedObject.ApplyModifiedProperties();
            if (property.objectReferenceValue == null) EditorGUIUtility.ExitGUI();

            if (property.isExpanded)
            {
                // Draw a background that shows us clearly which fields are part of the ScriptableObject
                GUI.Box(
                    new Rect(0,
                        position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing - 1,
                        Screen.width,
                        position.height - EditorGUIUtility.singleLineHeight - EditorGUIUtility.standardVerticalSpacing),
                    "");

                EditorGUI.indentLevel++;
                var data = (ScriptableObject) property.objectReferenceValue;
                SerializedObject serializedObject = new SerializedObject(data);

                // Iterate over all the values and draw them
                SerializedProperty prop = serializedObject.GetIterator();
                float y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                if (prop.NextVisible(true))
                {
                    do
                    {
                        // Don't bother drawing the class file
                        if (prop.name == "m_Script") continue;
                        float height = EditorGUI.GetPropertyHeight(prop, new GUIContent(prop.displayName), true);
                        EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), prop, true);
                        y += height + EditorGUIUtility.standardVerticalSpacing;
                    } while (prop.NextVisible(false));
                }

                if (GUI.changed)
                    serializedObject.ApplyModifiedProperties();

                EditorGUI.indentLevel--;
            }
        }
        else
        {
            EditorGUI.ObjectField(
                new Rect(position.x, position.y, position.width - 60, EditorGUIUtility.singleLineHeight), property);
            if (GUI.Button(
                new Rect(position.x + position.width - 58, position.y, 58, EditorGUIUtility.singleLineHeight),
                "Create"))
            {
                string selectedAssetPath = "Assets";
                if (property.serializedObject.targetObject is MonoBehaviour)
                {
                    MonoScript ms =
                        MonoScript.FromMonoBehaviour((MonoBehaviour) property.serializedObject.targetObject);
                    selectedAssetPath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(ms));
                }

                Type type = fieldInfo.FieldType;
                if (type.IsArray) type = type.GetElementType();
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    type = type.GetGenericArguments()[0];
                property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);
            }
        }

        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }

    // Creates a new ScriptableObject via the default Save File panel
    ScriptableObject CreateAssetWithSavePrompt(Type type, string path)
    {
        path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject", "New " + type.Name + ".asset", "asset",
            "Enter a file name for the ScriptableObject.", path);
        if (path == "") return null;
        ScriptableObject asset = ScriptableObject.CreateInstance(type);
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        EditorGUIUtility.PingObject(asset);
        return asset;
    }
}


public class ScriptableVariable : ScriptableObject
{
}