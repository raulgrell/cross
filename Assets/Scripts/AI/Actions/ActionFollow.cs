using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unit;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow", menuName = "FSM/Action/Follow")]
public class ActionFollow : UnitAction
{
    public float actionTime;
    public float variance;
    
    private float timer;

    private void OnEnable()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        timer = Random.Range(actionTime - variance, actionTime + variance);
    }

    public override void Act(UnitController agent)
    {
        if (timer < 0)
        {
            if (agent.IsAtWaypoint())
                agent.GoToNextWaypoint();
            
            ResetTimer();
        }

        timer -= Time.deltaTime;
    }
}
