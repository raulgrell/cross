using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Task
{
    public override TaskStatus Run(Agent agent, World wm)
    {
        int failureCount = 0;
        foreach (Task task in children)
        {
            if (task.status != TaskStatus.Failure)
            {
                TaskStatus childrenStatus = task.Run(agent, wm);
                if (childrenStatus == TaskStatus.Success)
                {
                    status = TaskStatus.Success;
                    return status;
                }
                else if (childrenStatus == TaskStatus.Failure)
                {
                    failureCount++;
                }
            }
            else
            {
                break;
            }
        }

        status = failureCount == children.Count ? TaskStatus.Failure : TaskStatus.Running;
        return status;
    }
}