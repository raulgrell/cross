using UnityEngine;
using UnityEngine.AI;

public class FollowAction : Task
{
    public FollowAction(string dest)
    {
    }

    public override TaskStatus Run(Agent agent, World wm)
    {
        if (status == TaskStatus.None)
        {
            status = TaskStatus.Running;
        }
        status = TaskStatus.Success;

        return status;
    }

    private bool IsAtDestination(NavMeshAgent navMeshAgent)
    {
        return false;
    }
}