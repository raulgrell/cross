using System;
using QAI;
using UnityEngine;

[Serializable]
public class Vector2IntReference : Reference<Vector2Int, Vector2Variable>
{
    public Vector2IntReference()
    {
    }

    public Vector2IntReference(Vector2Int Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Vector2")]
public class Vector2Variable : Variable<Vector2Int>
{
}

[CreateNodeMenuAttribute("Variable/Vector2")]
public class Vector2Node : VariableNode<Vector2Int>
{
}