using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(fileName = "Aim", menuName = "FSM/Action/Aim")]
public class ActionAim : UnitAction
{
    public override bool Act(UnitController agent)
    {
        agent.LookAtTarget();
        return true;
    }
}
