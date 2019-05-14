using UnityEngine;
using UnityEngine.AI;

public class AttackAction : Task
{
    public AttackAction(string dest)
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