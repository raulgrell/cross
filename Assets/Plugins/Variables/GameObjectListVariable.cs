using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectListReference : Reference<List<GameObject>, GameObjectListVariable>
{
    public GameObjectListReference() { }
    public GameObjectListReference(List<GameObject> Value) : base(Value) { }
}

[CreateAssetMenu(menuName = "Variable/Object List")]
public class GameObjectListVariable : Variable<List<GameObject>> { }
