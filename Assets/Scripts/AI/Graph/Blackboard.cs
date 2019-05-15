using UnityEngine;

public class Blackboard : ScriptableObject
{
    public class StringDictionary : SerializableDictionary<string, Variable> {}
    [SerializeField] public StringDictionary variables = new StringDictionary();
    
    public T GetValue<T>(string key)
    {
        if (variables.TryGetValue(key, out Variable value))
            return value.GetType().IsAssignableFrom(typeof(T)) ? (T)value.value : default;

        return default;
    }
}