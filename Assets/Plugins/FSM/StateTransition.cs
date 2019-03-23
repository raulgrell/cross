using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public class StateTransition : ScriptableObject
{
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;
    [SerializeField] private State target;

    public State Target => target;
    public StateAction Action => action;

    public bool IsTriggered<T>(FiniteStateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}