using System;
using UnityEngine;

[Serializable]
public class FloatReference : Reference<float, FloatVariable>
{
    public FloatReference()
    {
    }

    public FloatReference(float Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : Variable<float>
{
}