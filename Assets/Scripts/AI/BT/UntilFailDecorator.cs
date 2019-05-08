using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntilFailDecorator : Decorator
{
    public override TaskStatus Run(Agent agent, World wordManager)
    {
        if (status == TaskStatus.None) status = TaskStatus.Running;
        if (child.Run(agent, wordManager) == TaskStatus.Failure) status = TaskStatus.Success;
        return status;
    }
}