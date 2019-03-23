using System;
using UnityEngine;

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