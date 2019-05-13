using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAction : Task
{
    private string doorName;

    public OpenDoorAction(string door)
    {
        doorName = door;
    }

    public override TaskStatus Run(Agent agent, World wordManager)
    {
        if (!wordManager.DoorIsOpen(doorName))
        {
            wordManager.OpenDoor(doorName);
        }

        status = TaskStatus.Success;
        return status;
    }
}