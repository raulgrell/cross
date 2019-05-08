using System;
using UnityEngine;
using XNode;

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

[Serializable]
[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : Variable<float>
{
}

[Serializable]
[CreateNodeMenuAttribute("Variable/Float")]
public class FloatNode : VariableNode<float>
{
}