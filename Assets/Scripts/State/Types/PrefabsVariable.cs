using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
public class PrefabsReference : Reference<List<GameObject>, PrefabsVariable>
{
    public PrefabsReference()
    {
    }

    public PrefabsReference(List<GameObject> Value) : base(Value)
    {
    }
}

[CreateAssetMenu(menuName = "Variable/Prefabs")]
public class PrefabsVariable : Variable<List<GameObject>>
{
}

[CreateNodeMenuAttribute("Variable/Prefabs")]
public class PrefabsNode : VariableNode<List<GameObject>>
{
}