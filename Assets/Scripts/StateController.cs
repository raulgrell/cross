using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] waypoints;

    private int waypointIndex;
    private float energy;
    private NavMeshAgent agent;

    public Transform Target => target;
    public float Energy => energy;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        
        waypointIndex = 0;
        energy = 10;
    }

    public void GoToNextWaypoint()
    {
        agent.destination = waypoints[waypointIndex].position;
        waypointIndex += 1;

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    public void GoToTarget()
    {
        agent.destination = target.position;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public bool isAtDestination()
    {
        return !agent.pathPending
               && agent.remainingDistance <= agent.stoppingDistance
               && (!agent.hasPath || agent.velocity.sqrMagnitude == 0);
    }
}