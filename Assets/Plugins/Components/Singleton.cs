using UnityEngine;
 
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool quitting;
    private static readonly object safety = new object();
 
    public static T Instance
    {
        get
        {
            if (quitting)
                return null;
 
            lock (safety)
            {
                if (instance != null)
                    return instance;
                
                instance = FindObjectOfType<T>();
                
                if (instance != null)
                    return instance;

                var typename = typeof(T).ToString();
                var singletonObject = new GameObject(typename) {name = typeof(T).Name};
                instance = singletonObject.AddComponent<T>();
                DontDestroyOnLoad(singletonObject);

                return instance;
            }
        }
    }
 
 
    private void OnApplicationQuit()
    {
        quitting = true;
    }
 
 
    private void OnDestroy()
    {
        quitting = true;
    }
}