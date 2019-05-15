using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UnitAction : StateAction
{
    public event Action OnComplete;
    
    public override bool Act(StateMachine fsm)
    {
        if (Act(fsm.Agent))
        {
            OnComplete?.Invoke();
            return true;
        };
        return false;
    }

    public abstract bool Act(UnitController agent);
}
