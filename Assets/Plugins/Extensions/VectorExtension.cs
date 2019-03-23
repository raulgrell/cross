using UnityEngine;

public static class VectorExtension
{
    public static Vector2 Clamp(this Vector2 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        return target;
    }

    public static Vector2 Clamp(this Vector2 target, Vector2 min, Vector2 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        return target;
    }

    public static Vector3 Clamp(this Vector3 target, float min, float max)
    {
        target.x = Mathf.Clamp(target.x, min, max);
        target.y = Mathf.Clamp(target.y, min, max);
        target.z = Mathf.Clamp(target.z, min, max);
        return target;
    }

    public static Vector3 Clamp(this Vector3 target, Vector3 min, Vector3 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);
        return target;
    }

    public static Vector2 xy(this Vector3 target)
    {
        return new Vector2(target.x, target.y);
    }

    public static Vector3 xyz(this Vector2 target, float z)
    {
        return new Vector3(target.x, target.y, z);
    }

    public static Vector3 xyz(this Vector3 target, float z)
    {
        return new Vector3(target.x, target.y, z);
    }
}