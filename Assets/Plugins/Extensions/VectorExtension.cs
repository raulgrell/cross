using UnityEngine;

public static class VectorExtension
{
    public static UnityEngine.Vector2 Clamp(this UnityEngine.Vector2 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        return target;
    }

    public static UnityEngine.Vector2 Clamp(this UnityEngine.Vector2 target, UnityEngine.Vector2 min, UnityEngine.Vector2 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        return target;
    }

    public static UnityEngine.Vector3 Clamp(this UnityEngine.Vector3 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        target.z = Mathf.Clamp(target.z, min, max);
        return target;
    }

    public static UnityEngine.Vector3 Clamp(this UnityEngine.Vector3 target, UnityEngine.Vector3 min, UnityEngine.Vector3 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);
        return target;
    }

    public static UnityEngine.Vector2 xy(this UnityEngine.Vector3 target)
    {
        return new UnityEngine.Vector2(target.x, target.y);
    }

    public static UnityEngine.Vector3 xyz(this UnityEngine.Vector2 target, float z)
    {
        return new UnityEngine.Vector3(target.x, target.y, z);
    }

    public static UnityEngine.Vector3 xyz(this UnityEngine.Vector3 target, float z)
    {
        return new UnityEngine.Vector3(target.x, target.y, z);
    }
}