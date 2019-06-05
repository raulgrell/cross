using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Wait", menuName = "FSM/Action/Task")]
public class ActionTask : UnitAction
{
    [SerializeField] private Task task;

    public override bool Act(UnitController agent)
    {
        return (task.Run(agent, World.Instance) == TaskStatus.Success);
    }
}
