using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus
{
    None,
    Success,
    Failure,
    Running
};

public abstract class Task : ScriptableObject
{
    protected readonly List<Task> children;
    public TaskStatus status;
    
    public abstract TaskStatus Run(UnitController unit, World wm);

    protected Task()
    {
        children = new List<Task>();
        status = TaskStatus.None;
    }

    public void AddChildren(Task task)
    {
        children.Add(task);
    }
}