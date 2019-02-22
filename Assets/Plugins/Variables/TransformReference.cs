using System;
using UnityEngine;

[Serializable]
public class TransformReference : Reference<string, TransformVariable>
{
    public TransformReference() { }
    public TransformReference(string Value) : base(Value) { }
}

[CreateAssetMenu(menuName = "Variable/Transform")]
public class TransformVariable : Variable<string> { }