using System;
using UnityEngine;
using XNode;

[NodeTint("#FFBABB")]
public abstract class VariableNode : Node
{
    public abstract object GetValue();
}
    
public abstract class VariableNode<T> : VariableNode
{
    [Output(ShowBackingValue.Always)]
    public T _value;
 
    public override object GetValue(NodePort port)
    {
        // Only respond to value requests.
        if (port.fieldName != "_value")
            return null;
 
        T value = (graph as AIGraph).Blackboard.GetValue<T>(name);
        return value;
    }
 
    public override object GetValue()
    {
        NodePort port = GetOutputPort("_value");
        return GetValue(port);
    }
}

[Serializable]
public abstract class Variable : ScriptableObject
{
    public object value;
}

public abstract class Variable<T> : Variable
{
    public T Value;

    public void SetValue(T val)
    {
        Value = val;
    }

    public void SetValue(Variable<T> val)
    {
        Value = val.Value;
    }
}