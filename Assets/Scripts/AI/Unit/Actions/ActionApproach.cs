using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow", menuName = "FSM/Action/Follow")]
public class ActionApproach : UnitAction
{
    public float actionTime;
    [Range(0.1f, 0.9f)] public float variance;
    
    public override bool Act(UnitController agent)
    {
        if (!agent.Ready)
            return false;

        agent.MoveTowardsTarget();
        agent.ResetTimer(Random.Range(actionTime * (1 - variance), actionTime * (1 + variance)));

        return false;
    }
}
