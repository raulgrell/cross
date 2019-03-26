using System;
using QAI;
using UnityEditor;
using UnityEngine;
using XNode;

[Serializable]
public class BoolReference : Reference<bool, BoolVariable>
{
    public BoolReference()
    {
    }

    public BoolReference(bool Value) : base(Value)
    {
    }
}


[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : Variable<bool>
{
}

[CreateNodeMenuAttribute("Variable/Bool")]
public class BoolNode : VariableNode<bool>
{
}