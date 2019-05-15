using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BehaviourTree<T> : MonoBehaviour where T : UnitController
{
    [SerializeField] private World world;
    [SerializeField] private TaskGraph graph;

    private TaskStatus status = TaskStatus.None;
    private Task current;

    private T unit;

    protected void Start()
    {
        unit = GetComponent<T>();
        current = graph.Root;
    }

    void Update()
    {
        if (status == TaskStatus.None || status == TaskStatus.Running)
            status = current.Run(unit, world);
    }    
}
