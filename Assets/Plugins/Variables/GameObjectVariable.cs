using System;
using UnityEngine;

[Serializable]
public class GameObjectReference : Reference<GameObject, GameObjectVariable>
{
    public GameObjectReference() { }
    public GameObjectReference(GameObject Value) : base(Value) { }
}

[CreateAssetMenu(menuName = "Variable/Game Object")]
public class GameObjectVariable : Variable<GameObject> { }