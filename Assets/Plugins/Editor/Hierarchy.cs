namespace UnityEditorSnippets {
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using UnityEditor;

    public class MakeFirstChild {
        [MenuItem("GameObject/Make first child", false, -5)]
        static void makeFirstChild(MenuCommand command) {
            var go = command.context as GameObject;
            if (go.transform.parent) {
                Undo.RegisterFullObjectHierarchyUndo(go.transform.parent, "Make first child");
                go.transform.SetAsFirstSibling();
            }
            else {
                go.transform.SetSiblingIndex(0);
            }
        }
        
        [MenuItem("GameObject/Make last child", false, -5)]
        static void makeLastChild(MenuCommand command) {
            var go = command.context as GameObject;
            if (go.transform.parent) {
                Undo.RegisterFullObjectHierarchyUndo(go.transform.parent, "Make last child");
                go.transform.SetAsLastSibling();
            }
            else {
                go.transform.SetSiblingIndex(int.MaxValue);
            }
        }
        
        [MenuItem("GameObject/Move down", false, -4)]
        static void moveDown(MenuCommand command) {
            var go = command.context as GameObject;
            if (go.transform.parent) {
                Undo.RegisterFullObjectHierarchyUndo(go.transform.parent, "Move down");
                var index = go.transform.GetSiblingIndex();
                go.transform.SetSiblingIndex(index < go.transform.parent.childCount ? index + 1 : index);
            }
            else {
                var index = go.transform.GetSiblingIndex();
                go.transform.SetSiblingIndex(index + 1);
            }
        }
        
        [MenuItem("CONTEXT/Component/Move to bottom")]
        static void moveToBottom(MenuCommand command) {
            for (int i = 0; i < 100; i++) {
                UnityEditorInternal.ComponentUtility.MoveComponentDown(command.context as Component);
            }
        }
        
        [MenuItem("CONTEXT/Component/Move to top")]
        static void moveToTop(MenuCommand command) {
            for (int i = 0; i < 100; i++) {
                UnityEditorInternal.ComponentUtility.MoveComponentUp(command.context as Component);
            }
        }
        
        [MenuItem("GameObject/Move up", false, -4)]
        static void moveUp(MenuCommand command) {
            var go = command.context as GameObject;
            if (go.transform.parent) {
                Undo.RegisterFullObjectHierarchyUndo(go.transform.parent, "Move up");
                var index = go.transform.GetSiblingIndex();
                go.transform.SetSiblingIndex(index > 0 ? index - 1 : index);
            }
            else {
                var index = go.transform.GetSiblingIndex();
                go.transform.SetSiblingIndex(index > 0 ? index - 1 : index);
            }
        }
        
        [MenuItem("GameObject/Move up one level", false, -4)]
        static void moveUpOneLevel(MenuCommand command) {
            var go = command.context as GameObject;
            Transform parent = go.transform.parent;
            if(parent != null) {
                Undo.SetTransformParent(go.transform, parent.parent, "Move up one level");
            }
        }
    }
}