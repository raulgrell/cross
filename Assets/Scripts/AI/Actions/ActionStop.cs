using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "Stop", menuName = "FSM/Action/Stop")]
public class ActionStop : UnitAction 
{
    public override bool Act(UnitController agent)
    {
        return false;
    }
}
