using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class StateTransition
{
    [SerializeField] private StateVariable target;
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;

    public StateVariable Target => target;
    public StateAction Action => action;

    public bool IsTriggered<T>(StateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}