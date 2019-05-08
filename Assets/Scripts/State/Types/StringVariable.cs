using System;
using UnityEngine;
using XNode;

[Serializable]
public class StringReference : Reference<string, StringVariable>
{
    public StringReference()
    {
    }

    public StringReference(string Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/String")]
public class StringVariable : Variable<string>
{
}

[CreateNodeMenuAttribute("Variable/String")]
public class StringNode : VariableNode<string>
{
}