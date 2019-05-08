using System;
using UnityEngine;

[Serializable]
public class Vector3Reference : Reference<Vector3, Vector3Variable>
{
    public Vector3Reference()
    {
    }

    public Vector3Reference(Vector3 Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Vector3")]
public class Vector3Variable : Variable<Vector3>
{
}


[CreateNodeMenuAttribute("Variable/Vector3")]
public class Vector3Node : VariableNode<Vector3>
{
}