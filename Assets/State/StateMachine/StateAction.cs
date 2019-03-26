using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class StateAction : ScriptableObject
{
    public abstract void Act<T>(StateMachine<T> fsm) where T : MonoBehaviour;
}