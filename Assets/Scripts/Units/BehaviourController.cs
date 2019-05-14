using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GridUnit))]
public class BehaviourController : UnitController
{
    [SerializeField] private World world;
    private Task behaviorTree;
    private TaskStatus status = TaskStatus.None;

    protected override void Start()
    {
        base.Start();
        Task sequenceMoveToRoom = new Sequence();
        sequenceMoveToRoom.AddChildren(new DoorOpenCondition("Door1"));
        sequenceMoveToRoom.AddChildren(new FollowAction("Room1"));

        Task sequenceOpenDoorMoveToRoom = new Sequence();
        sequenceOpenDoorMoveToRoom.AddChildren(new FollowAction("Door1"));
        sequenceOpenDoorMoveToRoom.AddChildren(new FollowAction("Room1"));
        
        behaviorTree = new Selector();
        behaviorTree.AddChildren(sequenceMoveToRoom);
        behaviorTree.AddChildren(sequenceOpenDoorMoveToRoom);
    }

    void Update()
    {
        if (status == TaskStatus.None || status == TaskStatus.Running)
            status = behaviorTree.Run(this, world);
    }    
}
