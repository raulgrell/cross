using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unit;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "FSM/Action/Attack")]
public class ActionAttack : UnitAction
{
    [Expandable]
    public UnitData data;

    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    public override void Act(UnitController agent)
    {
        timer += Time.deltaTime;
    }
}
