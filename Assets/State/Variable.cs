using UnityEngine;

public abstract class Variable : ScriptableObject
{
}

public class Variable<T> : Variable
{
    public T Value;

    public void SetValue(T value)
    {
        Value = value;
    }

    public void SetValue(Variable<T> value)
    {
        Value = value.Value;
    }
}