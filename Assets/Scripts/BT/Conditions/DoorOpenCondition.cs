using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCondition : Task
{
    private string doorName;

    public DoorOpenCondition(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(Agent agent, World wm)
    {
        status = wm.DoorIsOpen(doorName) ? TaskStatus.Success : TaskStatus.Failure;
        return status;
    }
}