using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class StateAction : ScriptableObject
{
    public abstract void Act<T>(FiniteStateMachine<T> fsm) where T : MonoBehaviour;
}
