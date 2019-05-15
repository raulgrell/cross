using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonDeterministicSequence : Task
{
    private bool shuffledOrder;

    public NonDeterministicSequence()
    {
        shuffledOrder = false;
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (!shuffledOrder)
        {
            Shuffle(children);
            shuffledOrder = true;
        }

        int successCount = 0;
        foreach (Task task in children)
        {
            if (task.status != TaskStatus.Success)
            {
                TaskStatus childrenStatus = task.Run(unit, wm);
                if (childrenStatus == TaskStatus.Failure)
                {
                    status = TaskStatus.Failure;
                    return status;
                }
                
                if (childrenStatus == TaskStatus.Success)
                    successCount++;
                else
                    break;
            }
            else
            {
                successCount++;
            }
        }

        status = successCount == children.Count ? TaskStatus.Success : TaskStatus.Running;
        return status; 
    }

    public void Shuffle(List<Task> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            Task value = list[k];
            list[k] = list[n];
            list[n] = value;
            n--;
        }
    }
}