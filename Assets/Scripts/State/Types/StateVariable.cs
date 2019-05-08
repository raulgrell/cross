using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
public class StateReference : Reference<State, StateVariable>
{
    public StateReference()
    {
    }

    public StateReference(State Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/State")]
public class StateVariable : Variable<State>
{
}

[CreateNodeMenuAttribute("Variable/State")]
public class StateVariableNode : VariableNode<State>
{
}