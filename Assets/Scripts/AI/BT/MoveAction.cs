using UnityEngine;
using UnityEngine.AI;

public class MoveAction : Task
{
    private string destionation;

    public MoveAction(string dest)
    {
        destionation = dest;
    }

    public override TaskStatus Run(Agent agent, World wordManager)
    {
        NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
        Vector3 dest = wordManager.GetWaypoint(destionation).position;
        if (status == TaskStatus.None)
        {
            navMeshAgent.destination = dest;
            status = TaskStatus.Running;
        }
        else if (IsAtDestionation(navMeshAgent))
        {
            status = TaskStatus.Success;
        }

        return status;
    }

    private bool IsAtDestionation(NavMeshAgent navMeshAgent)
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}