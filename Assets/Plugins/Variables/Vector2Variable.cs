using System;
using UnityEngine;

[Serializable]
public class Vector2Reference : Reference<Vector2, Vector2Variable>
{
    public Vector2Reference() { }
    public Vector2Reference(Vector2 Value) : base(Value) { }
}

[CreateAssetMenu(menuName = "Variable/Vector2")]
public class Vector2Variable : Variable<Vector2> { }