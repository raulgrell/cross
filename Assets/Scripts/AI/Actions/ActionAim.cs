using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow", menuName = "FSM/Action/Follow")]
public class ActionAim : UnitAction
{
    public override bool Act(UnitController agent)
    {
        Debug.Log("Aim");
        return false;
    }
}
