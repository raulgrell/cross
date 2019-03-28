using System;
using System.Collections;
using System.Collections.Generic;
using QAI;
using UnityEngine;
using XNode;

[Serializable]
public class ConditionReference : Reference<StateCondition, StateConditionVariable>
{
    public ConditionReference()
    {
    }

    public ConditionReference(StateCondition Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Condition")]
public class StateConditionVariable : Variable<StateCondition>
{
}

[CreateNodeMenuAttribute("Variable/Condition")]
public class StateConditionNode : VariableNode<StateCondition>
{
}