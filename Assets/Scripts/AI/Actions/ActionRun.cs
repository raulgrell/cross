using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "FSM/Action/Run")]
public class ActionRun: UnitAction 
{
    public override void Act(UnitController agent)
    {
        agent.MoveAwayFromTarget();
    }
}
