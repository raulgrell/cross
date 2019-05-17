using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
internal class TreeTask : TreeElement
{
    protected readonly List<TreeTask> tasks;
    public TaskStatus status;
    public string text;
    public bool enabled;

    public TreeTask(string name, int depth, int id) : base(name, depth, id)
    {
        tasks = new List<TreeTask>();
        status = TaskStatus.None;
        enabled = true;
    }

    public virtual TaskStatus Run(UnitController unit, World wm)
    {
        return TaskStatus.Failure;
    }

    public void AddChildren(TreeTask task)
    {
        tasks.Add(task);
    }
}