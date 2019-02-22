
using System;
using UnityEditor;
using UnityEngine;

public static class RGUX
{
    [MenuItem("RGUX/Selection/Select Parent _\\")]
    static void SelectParentOfCurrentSelection()
    {
        if (Selection.activeGameObject == null ||
            Selection.activeGameObject.transform.parent == null)
        {
            return;
        }
        Selection.activeGameObject = Selection.activeGameObject.transform.parent.gameObject;
    }
}