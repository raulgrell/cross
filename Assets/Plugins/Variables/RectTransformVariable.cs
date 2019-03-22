using System;
using UnityEngine;

[Serializable]
public class RectTransformReference : Reference<RectTransform, RectTransformVariable>
{
    public RectTransformReference()
    {
    }

    public RectTransformReference(RectTransform Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/RectTransform")]
public class RectTransformVariable : Variable<RectTransform>
{
}