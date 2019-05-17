using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

internal class TaskTreeView : TreeViewModel<TreeTask>
{
    static class Styles
    {
        public static readonly GUIStyle background = "RL Background";
        public static readonly GUIStyle headerBackground = "RL Header";
    }

    public TaskTreeView(TreeViewState state, TreeModel<TreeTask> model)
        : base(state, model)
    {
        showBorder = true;
        customFoldoutYOffset = 3f;
        Reload();
    }

    protected override float GetCustomRowHeight(int row, TreeViewItem item)
    {
        var myItem = (TreeViewItem<TreeTask>) item;
        return myItem.data.enabled ? 85f : 30f;
    }

    public override void OnGUI(Rect rect)
    {
        if (Event.current.type == EventType.Repaint)
            DefaultStyles.backgroundOdd.Draw(rect, false, false, false, false);

        base.OnGUI(rect);
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        var item = (TreeViewItem<TreeTask>) args.item;
        var contentIndent = GetContentIndent(item);

        // Background
        var bgRect = args.rowRect;
        bgRect.x = contentIndent;
        bgRect.width = Mathf.Max(bgRect.width - contentIndent, 155f) - 5f;
        bgRect.yMin += 2f;
        bgRect.yMax -= 2f;
        DrawItemBackground(bgRect);

        // Custom label
        var headerRect = bgRect;
        headerRect.xMin += 5f;
        headerRect.xMax -= 10f;
        headerRect.height = Styles.headerBackground.fixedHeight;
        HeaderGUI(headerRect, args.label, item);

        // Controls
        var controlsRect = headerRect;
        controlsRect.xMin += 20f;
        controlsRect.y += headerRect.height;
        if (item.data.enabled)
            ControlsGUI(controlsRect, item);
    }

    void DrawItemBackground(Rect bgRect)
    {
        if (Event.current.type != EventType.Repaint)
            return;
        
        var rect = bgRect;
        rect.height = Styles.headerBackground.fixedHeight;
        Styles.headerBackground.Draw(rect, false, false, false, false);

        rect.y += rect.height;
        rect.height = bgRect.height - rect.height;
        Styles.background.Draw(rect, false, false, false, false);
    }

    void HeaderGUI(Rect headerRect, string label, TreeViewItem<TreeTask> item)
    {
        headerRect.y += 1f;

        Rect toggleRect = headerRect;
        toggleRect.width = 16;
        EditorGUI.BeginChangeCheck();
        item.data.enabled = EditorGUI.Toggle(toggleRect, item.data.enabled);
        if (EditorGUI.EndChangeCheck())
            RefreshCustomRowHeights();

        Rect labelRect = headerRect;
        labelRect.xMin += toggleRect.width + 2f;
        GUI.Label(labelRect, label);
    }

    void ControlsGUI(Rect controlsRect, TreeViewItem<TreeTask> item)
    {
        var rect = controlsRect;
        rect.y += 3f;
        rect.height = EditorGUIUtility.singleLineHeight;
        item.data.text = GUI.TextField(rect, item.data.text);
    }

    protected override Rect GetRenameRect(Rect rowRect, int row, TreeViewItem item)
    {
        var renameRect = base.GetRenameRect(rowRect, row, item);
        renameRect.xMin += 25f;
        renameRect.y += 2f;
        return renameRect;
    }

    protected override bool CanRename(TreeViewItem item)
    {
        // Only allow rename if we can show the rename overlay with a certain width (label might be clipped by other columns)
        Rect renameRect = GetRenameRect(treeViewRect, 0, item);
        return renameRect.width > 30;
    }

    protected override void RenameEnded(RenameEndedArgs args)
    {
        if (!args.acceptedRename)
            return;
        
        // Set the backend name and reload the tree to reflect the new model
        var element = treeModel.Find(args.itemID);
        element.name = args.newName;
        Reload();
    }
}