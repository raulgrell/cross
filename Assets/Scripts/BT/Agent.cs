using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private World wordManager;
    private Task behaviorTree;
    private TaskStatus behaviorTreeStatus = TaskStatus.None;

    void Start()
    {
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
        if ((behaviorTreeStatus == TaskStatus.None) || (behaviorTreeStatus == TaskStatus.Running))
        {
            behaviorTreeStatus = behaviorTree.Run(this, wordManager);
        }
    }
}