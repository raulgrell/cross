using UnityEngine;
using UnityEngine.AI;

public class AimAction : Task
{
    public AimAction(string dest)
    {
    }

    public override TaskStatus Run(UnitController unit, World wm)
    {
        if (status == TaskStatus.None)
        {
            status = TaskStatus.Running;
        }
        status = TaskStatus.Success;

        return status;
    }
}