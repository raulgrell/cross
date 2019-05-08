using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "FSM/Action/Grab")]
public class ActionGrab: UnitAction 
{
    public override bool Act(UnitController agent)
    {
        agent.GrabTarget();
        return false;
    }
}
