using System;
using QAI;
using UnityEngine;
using XNode;

[Serializable]
public class PrefabReference : Reference<GameObject, PrefabVariable>
{
    public PrefabReference()
    {
    }

    public PrefabReference(GameObject Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Prefab")]
public class PrefabVariable : Variable<GameObject>
{
}

[CreateNodeMenuAttribute("Variable/Prefab")]
public class PrefabNode : VariableNode<GameObject>
{
}