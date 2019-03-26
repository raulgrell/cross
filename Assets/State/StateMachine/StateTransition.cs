using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public class StateTransition : ScriptableObject
{
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;
    [SerializeField] private StateVariable target;

    public State Target => target.Value;
    public StateAction Action => action;

    public bool IsTriggered<T>(StateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}