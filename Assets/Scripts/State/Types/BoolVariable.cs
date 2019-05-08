using System;
using UnityEngine;

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
public class BoolTest : VariableNode<bool>
{
}

[CreateNodeMenuAttribute("Variable/Bool")]
public class BoolNode : VariableNode<bool>
{
}