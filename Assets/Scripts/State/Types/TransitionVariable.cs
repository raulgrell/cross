using System;
using System.Collections;
using System.Collections.Generic;
using QAI;
using UnityEngine;
using XNode;

[Serializable]
public class StateTransitionReference : Reference<StateTransition, StateTransitionVariable>
{
    public StateTransitionReference()
    {
    }

    public StateTransitionReference(StateTransition Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Transition")]
public class StateTransitionVariable : Variable<StateTransition>
{
}

[CreateNodeMenuAttribute("Variable/Transition")]
public class StateTransitionNode : VariableNode<StateTransition>
{
}