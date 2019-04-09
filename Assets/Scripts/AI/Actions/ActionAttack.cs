using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(fileName = "Attack", menuName = "FSM/Action/Attack")]
public class ActionAttack : UnitAction
{
    public float actionTime;
    
    [Range(0.1f, 0.9f)] public float variance;
    
    private float timer;

    private void OnEnable()
    {
        ResetTimer();
    }

    public override bool Act(UnitController agent)
    {
        if (timer < 0)
        {
            agent.LookAtTarget();
            agent.Attack();
            ResetTimer();
        }

        timer -= Time.deltaTime;

        return false;
    }
    
    public void ResetTimer()
    {
        timer = Random.Range(actionTime * (1 - variance), actionTime * (1 + variance));
    }
}
