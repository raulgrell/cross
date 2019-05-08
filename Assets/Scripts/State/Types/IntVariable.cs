using System;
using UnityEngine;
using XNode;

[Serializable]
public class IntReference : Reference<int, IntVariable>
{
    public IntReference()
    {
    }

    public IntReference(int Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : Variable<int>
{
}

[CreateNodeMenuAttribute("Variable/Int")]
public class IntNode : VariableNode<int>
{
}