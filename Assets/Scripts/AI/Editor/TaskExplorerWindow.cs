using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

class TaskExplorerWindow : EditorWindow
{
    [NonSerialized] bool m_Initialized;

    [SerializeField]
    TreeViewState m_TreeViewState; // Serialized in the window layout file so it survives assembly reloading

    [SerializeField] MultiColumnHeaderState m_MultiColumnHeaderState;
    SearchField m_SearchField;
    MultiColumnTreeView mTreeView;
    TaskTree mTaskTree;

    [MenuItem("Custom/Task Explorer")]
    public static TaskExplorerWindow GetWindow()
    {
        var window = GetWindow<TaskExplorerWindow>();
        window.titleContent = new GUIContent("Multi Columns");
        window.Focus();
        window.Repaint();
        return window;
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var myTreeAsset = EditorUtility.InstanceIDToObject(instanceID) as TaskTree;
        if (myTreeAsset != null)
        {
            var window = GetWindow();
            window.SetTreeAsset(myTreeAsset);
            return true;
        }

        return false; // we did not handle the open
    }

    void SetTreeAsset(TaskTree taskTree)
    {
        mTaskTree = taskTree;
        m_Initialized = false;
    }

    public MultiColumnTreeView TreeView => mTreeView;

    Rect multiColumnTreeViewRect => new Rect(20, 30, position.width - 40, position.height - 60);
    Rect toolbarRect => new Rect(20f, 10f, position.width - 40f, 20f);
    Rect bottomToolbarRect => new Rect(20f, position.height - 18f, position.width - 40f, 16f);

    void InitIfNeeded()
    {
        if (!m_Initialized)
        {
            // Check if it already exists (deserialized from window layout file or scriptable object)
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

            bool firstInit = m_MultiColumnHeaderState == null;
            var headerState = MultiColumnTreeView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
            if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
                MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
            m_MultiColumnHeaderState = headerState;

            var multiColumnHeader = new TaskViewMultiColumnsHeader(headerState);
            if (firstInit)
                multiColumnHeader.ResizeToFit();

            var treeModel = new TreeModel<TreeTask>(GetData());

            mTreeView = new MultiColumnTreeView(m_TreeViewState, multiColumnHeader, treeModel);

            m_SearchField = new SearchField();
            m_SearchField.downOrUpArrowKeyPressed += mTreeView.SetFocusAndEnsureSelectedItem;

            m_Initialized = true;
        }
    }

    IList<TreeTask> GetData()
    {
        if (mTaskTree != null && mTaskTree.treeElements != null && mTaskTree.treeElements.Count > 0)
            return mTaskTree.treeElements;

        // generate some test data
        return MyTreeElementGenerator.GenerateRandomTree(130);
    }

    void OnSelectionChange()
    {
        if (!m_Initialized)
            return;

        var myTreeAsset = Selection.activeObject as TaskTree;
        if (myTreeAsset != null && myTreeAsset != mTaskTree)
        {
            mTaskTree = myTreeAsset;
            mTreeView.treeModel.SetData(GetData());
            mTreeView.Reload();
        }
    }

    void OnGUI()
    {
        InitIfNeeded();

        SearchBar(toolbarRect);
        DoTreeView(multiColumnTreeViewRect);
        BottomToolBar(bottomToolbarRect);
    }

    void SearchBar(Rect rect)
    {
        TreeView.searchString = m_SearchField.OnGUI(rect, TreeView.searchString);
    }

    void DoTreeView(Rect rect)
    {
        mTreeView.OnGUI(rect);
    }

    void BottomToolBar(Rect rect)
    {
        GUILayout.BeginArea(rect);

        using (new EditorGUILayout.HorizontalScope())
        {
            var style = "miniButton";
            
            if (GUILayout.Button("Expand All", style))
                TreeView.ExpandAll();
            
            if (GUILayout.Button("Collapse All", style))
                TreeView.CollapseAll();

            GUILayout.FlexibleSpace();
            GUILayout.Label(mTaskTree != null ? AssetDatabase.GetAssetPath(mTaskTree) : string.Empty);
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("values <-> controls", style))
                TreeView.showControls = !TreeView.showControls;
        }

        GUILayout.EndArea();
    }
}

internal class TaskViewMultiColumnsHeader : MultiColumnHeader
{
    public TaskViewMultiColumnsHeader(MultiColumnHeaderState state)
        : base(state)
    {
        canSort = false;
        height = DefaultGUI.minimumHeight;
    }
}
