using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

/// <summary>
/// Custom dock-able editor window that allows to change snap settings
/// </summary>
public class DockableSnapSettings : EditorWindow
{
    #region Snap Properties

    private static Vector3 MoveSnap
    {
        get =>
            new Vector3(EditorPrefs.GetFloat("MoveSnapX", 1f),
                EditorPrefs.GetFloat("MoveSnapY", 1f),
                EditorPrefs.GetFloat("MoveSnapZ", 1f));
        set
        {
            EditorPrefs.SetFloat("MoveSnapX", value.x);
            EditorPrefs.SetFloat("MoveSnapY", value.y);
            EditorPrefs.SetFloat("MoveSnapZ", value.z);
        }
    }

    private static float ScaleSnap
    {
        get => EditorPrefs.GetFloat("ScaleSnap", 1f);
        set => EditorPrefs.SetFloat("ScaleSnap", value);
    }

    private static float RotationSnap
    {
        get => EditorPrefs.GetFloat("RotationSnap", 1f);
        set => EditorPrefs.SetFloat("RotationSnap", value);
    }

    #endregion

    private static Type _snapType;
    private const BindingFlags BINDING = BindingFlags.Public | BindingFlags.Static;

    [MenuItem("Window/Dockable Snap")]
    static void Init()
    {
        var dockSnap = GetWindow<DockableSnapSettings>("Snap Settings");
        dockSnap.minSize = new Vector2(200, 80);
    }

    /// <summary>
    /// Initialize expensive objects here
    /// </summary>
    private static void InitializeValues()
    {
        _snapType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(t => t.FullName == "UnityEditor.SnapSettings");
    }

    /// <summary>
    /// Unity built-in method for drawing the content of the window
    /// </summary>
    void OnGUI()
    {
        MoveSnap = EditorGUILayout.Vector3Field("Move", MoveSnap);
        RotationSnap = EditorGUILayout.FloatField("Rotation", RotationSnap);
        ScaleSnap = EditorGUILayout.FloatField("Scale", ScaleSnap);

        if (GUI.changed)
        {
            UpdateSnapSettings();
        }
    }

    /// <summary>
    /// Responsible for updating the static SnapSettings values that the editor actually uses so the changes will take effect
    /// </summary>
    private void UpdateSnapSettings()
    {
        // make sure we have the type reference
        if (_snapType == null)
        {
            InitializeValues();
        }

        // set move snap value
        var move = _snapType.GetProperty("move", BINDING);
        move.SetValue(_snapType, MoveSnap, null);

        // set rotate snap value
        var scale = _snapType.GetProperty("scale", BINDING);
        scale.SetValue(_snapType, ScaleSnap, null);

        // set scale snap value
        var rotate = _snapType.GetProperty("rotation", BINDING);
        rotate.SetValue(_snapType, RotationSnap, null);
    }
}