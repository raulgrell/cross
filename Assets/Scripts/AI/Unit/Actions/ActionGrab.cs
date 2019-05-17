using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "FSM/Action/Grab")]
public class ActionGrab: UnitAction 
{
    public override bool Act(UnitController agent)
    {
        if (!agent.Ready)
            return false;
        
        agent.GrabTarget();
        return true;
    }
}
