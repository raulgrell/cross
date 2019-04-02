using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TargetEvent : SerializableCallback<Transform, bool>
{
}

[CreateAssetMenu(fileName = "Target", menuName = "FSM/Condition/Target")]
public class ConditionTarget : UnitCondition
{
    public TargetEvent func;

    public override bool Test(UnitController unit)
    {
        return func?.Invoke(unit.target) ?? false;
    }

    public static bool IsNull(Transform target)
    {
        return (target == null);
    }
}
