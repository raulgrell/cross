using System;
using QAI;
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

[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : Variable<float>
{
}

[CreateNodeMenuAttribute("Variable/Float")]
public class FloatNode : VariableNode<float>
{
}