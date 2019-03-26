using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject {
    static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            return _instance;
        }
    }
}