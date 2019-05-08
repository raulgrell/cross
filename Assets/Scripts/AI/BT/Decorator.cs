using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : Task
{
    protected Task child;

    public new void AddChildren(Task task)
    {
        child = task;
    }
}