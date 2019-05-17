using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(TaskTree))]
public class TaskTreeEditor : Editor
{
    TaskTreeView mTreeView;
    SearchField m_SearchField;
    const string kSessionStateKeyPrefix = "TVS";

    TaskTree asset => (TaskTree) target;

    void OnEnable()
    {
        Undo.undoRedoPerformed += OnUndoRedoPerformed;

        var treeViewState = new TreeViewState();
        var jsonState = SessionState.GetString(kSessionStateKeyPrefix + asset.GetInstanceID(), "");
        if (!string.IsNullOrEmpty(jsonState))
            JsonUtility.FromJsonOverwrite(jsonState, treeViewState);
        var treeModel = new TreeModel<TreeTask>(asset.treeElements);
        mTreeView = new TaskTreeView(treeViewState, treeModel);
        mTreeView.beforeDroppingDraggedItems += OnBeforeDroppingDraggedItems;
        mTreeView.Reload();

        m_SearchField = new SearchField();

        m_SearchField.downOrUpArrowKeyPressed += mTreeView.SetFocusAndEnsureSelectedItem;
    }


    void OnDisable()
    {
        Undo.undoRedoPerformed -= OnUndoRedoPerformed;

        SessionState.SetString(kSessionStateKeyPrefix + asset.GetInstanceID(), JsonUtility.ToJson(mTreeView.state));
    }

    void OnUndoRedoPerformed()
    {
        if (mTreeView != null)
        {
            mTreeView.treeModel.SetData(asset.treeElements);
            mTreeView.Reload();
        }
    }

    void OnBeforeDroppingDraggedItems(IList<TreeViewItem> draggedRows)
    {
        Undo.RecordObject(asset,
            string.Format("Moving {0} Item{1}", draggedRows.Count, draggedRows.Count > 1 ? "s" : ""));
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(5f);
        ToolBar();
        GUILayout.Space(3f);

        const float topToolbarHeight = 20f;
        const float spacing = 2f;
        float totalHeight = mTreeView.totalHeight + topToolbarHeight + 2 * spacing;
        Rect rect = GUILayoutUtility.GetRect(0, 10000, 0, totalHeight);
        Rect toolbarRect = new Rect(rect.x, rect.y, rect.width, topToolbarHeight);
        Rect multiColumnTreeViewRect = new Rect(rect.x, rect.y + topToolbarHeight + spacing, rect.width,
            rect.height - topToolbarHeight - 2 * spacing);
        SearchBar(toolbarRect);
        DoTreeView(multiColumnTreeViewRect);
    }

    void SearchBar(Rect rect)
    {
        mTreeView.searchString = m_SearchField.OnGUI(rect, mTreeView.searchString);
    }

    void DoTreeView(Rect rect)
    {
        mTreeView.OnGUI(rect);
    }

    void ToolBar()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            var style = "miniButton";
            if (GUILayout.Button("Expand All", style))
            {
                mTreeView.ExpandAll();
            }

            if (GUILayout.Button("Collapse All", style))
            {
                mTreeView.CollapseAll();
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Add Item", style))
            {
                Undo.RecordObject(asset, "Add Item To Asset");

                // Add item as child of selection
                var selection = mTreeView.GetSelection();
                TreeElement parent = (selection.Count == 1 ? mTreeView.treeModel.Find(selection[0]) : null) ??
                                     mTreeView.treeModel.root;
                int depth = parent != null ? parent.depth + 1 : 0;
                int id = mTreeView.treeModel.GenerateUniqueID();
                var element = new TreeTask("Item " + id, depth, id);
                mTreeView.treeModel.AddElement(element, parent, 0);

                // Select newly created element
                mTreeView.SetSelection(new[] {id}, TreeViewSelectionOptions.RevealAndFrame);
            }

            if (GUILayout.Button("Remove Item", style))
            {
                Undo.RecordObject(asset, "Remove Item From Asset");
                var selection = mTreeView.GetSelection();
                mTreeView.treeModel.RemoveElements(selection);
            }
        }
    }

    class TaskTreeView : TreeViewModel<TreeTask>
    {
        public TaskTreeView(TreeViewState state, TreeModel<TreeTask> model)
            : base(state, model)
        {
            showBorder = true;
            showAlternatingRowBackgrounds = true;
        }
    }
}