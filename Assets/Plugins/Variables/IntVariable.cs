using System;
using UnityEngine;

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