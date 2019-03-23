using System;
using UnityEngine;

[Serializable]
public class RectReference : Reference<Rect, RectVariable>
{
    public RectReference()
    {
    }

    public RectReference(Rect Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Rect")]
public class RectVariable : Variable<Rect>
{
}