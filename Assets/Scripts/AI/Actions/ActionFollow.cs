﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow", menuName = "FSM/Action/Follow")]
public class ActionFollow : UnitAction
{
    public float actionTime;
    [Range(0.1f, 0.9f)] public float variance;
    
    public override bool Act(UnitController agent)
    {
        if (agent.actionTimer < 0)
        {
            if (agent.AtCurrentWaypoint())
            {
                agent.GoToNextWaypoint();
                agent.ResetTimer(Random.Range(actionTime * (1 - variance), actionTime * (1 + variance)));
            }
        }

        agent.actionTimer -= Time.deltaTime;

        return false;
    }
}
