using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Visibility", menuName = "FSM/Condition/Visibility")]
public class ConditionVisibility : UnitCondition
{
    public float range;
    public LayerMask visibleMask;

    public override bool Test(UnitController unit)
    {
        var ray = new Ray(unit.transform.position, unit.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, range, visibleMask.value))
            return false;
        
        return hitInfo.transform == unit.target;
    }
}

