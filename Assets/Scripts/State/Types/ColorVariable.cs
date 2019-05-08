using System;
using UnityEngine;

[Serializable]
public class ColorReference : Reference<Color, ColorVariable>
{
    public ColorReference()
    {
    }

    public ColorReference(Color Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Color")]
public class ColorVariable : Variable<Color>
{
}

[CreateNodeMenuAttribute("Variable/Color")]
public class ColorNode : VariableNode<Color>
{
}