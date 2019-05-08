using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "Wait", menuName = "FSM/Action/Wait")]
public class ActionWait : UnitAction
{
    [SerializeField] private float time;
    
    public override bool Act(UnitController agent)
    {
        Debug.Log("Aim");
        return false;
    }
}
