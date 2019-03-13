﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class StateCondition : ScriptableObject
{
    public abstract bool Test<T>(FiniteStateMachine<T> fsm) where T : MonoBehaviour;
}
