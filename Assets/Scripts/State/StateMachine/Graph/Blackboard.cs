using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : ScriptableObject
{
    public class StringDictionary : SerializableDictionary<string, object>
    {
    }

    [Serializable]
    public class IntDictionary : SerializableDictionary<int, object>
    {
    }

    [Serializable]
    public class IntSet : SerializableHashSet<int>
    {
    }

    [SerializeField] public StringDictionary variables = new StringDictionary();
    [SerializeField] public IntDictionary states = new IntDictionary();
    [SerializeField] public HashSet<int> inits = new HashSet<int>();
}