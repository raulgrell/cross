using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


class TaskTreeWindow : EditorWindow
{
    [NonSerialized] bool m_Initialized;

    // Serialized in the window layout file so it survives assembly reloading
    [SerializeField] TreeViewState m_TreeViewState; 

    SearchField m_SearchField;
    TaskTreeView mTreeView;
    TaskTree mTaskTree;

    [MenuItem("Custom/Task View")]
    public static TaskTreeWindow GetWindow()
    {
        var window = GetWindow<TaskTreeWindow>();
        window.titleContent = new GUIContent("Task View");
        window.Focus();
        window.Repaint();
        return window;
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        // Only open task trees
        var myTreeAsset = EditorUtility.InstanceIDToObject(instanceID) as TaskTree;
        if (myTreeAsset == null) 
            return false;
        
        var window = GetWindow();
        window.SetTreeAsset(myTreeAsset);
        return true;
    }

    void SetTreeAsset(TaskTree taskTree)
    {
        mTaskTree = taskTree;
        m_Initialized = false;
    }

    Rect treeViewRect => new Rect(20, 30, position.width - 40, position.height - 60);
    Rect toolbarRect => new Rect(20f, 10f, position.width - 40f, 20f);
    Rect bottomToolbarRect => new Rect(20f, position.height - 18f, position.width - 40f, 16f);

    void InitIfNeeded()
    {
        if (m_Initialized) 
            return;
        
        // Check if it already exists (deserialized from window layout file or scriptable object)
        if (m_TreeViewState == null)
            m_TreeViewState = new TreeViewState();

        var treeModel = new TreeModel<TreeTask>(GetData());
        mTreeView = new TaskTreeView(m_TreeViewState, treeModel);

        m_SearchField = new SearchField();
        m_SearchField.downOrUpArrowKeyPressed += mTreeView.SetFocusAndEnsureSelectedItem;

        m_Initialized = true;
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
        DoTreeView(treeViewRect);
        BottomToolBar(bottomToolbarRect);
    }

    void SearchBar(Rect rect)
    {
        mTreeView.searchString = m_SearchField.OnGUI(rect, mTreeView.searchString);
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
                mTreeView.ExpandAll();

            if (GUILayout.Button("Collapse All", style))
                mTreeView.CollapseAll();
        }

        GUILayout.EndArea();
    }
}