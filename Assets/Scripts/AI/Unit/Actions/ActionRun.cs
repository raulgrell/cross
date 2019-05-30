﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "FSM/Action/Run")]
public class ActionRun: UnitAction 
{
    public override bool Act(UnitController agent)
    {
        if (!agent.Ready)
            return false;
        
        agent.MoveAwayFromTarget();
        return true;
    }
}
