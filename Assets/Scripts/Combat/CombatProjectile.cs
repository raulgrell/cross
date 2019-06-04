using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CombatProjectile : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float lifetime;
    
    [Range(0.1f, 10f)]
    public float factor;

    public UnityEvent OnComplete;

    public static CombatProjectile Spawn(GameObject prefab, Vector3 start, Vector3 end)
    {
        var gameObject = Instantiate(prefab, start, Quaternion.identity);
        var projectile = gameObject.GetComponent<CombatProjectile>();
        return projectile;
    }

    private void Update()
    {
        transform.position = Bezier2(start, (start + end / 2).SetY(start.y), end, lifetime);
        lifetime += Time.deltaTime * factor;
        if (lifetime > 1)
        {
            Destroy(gameObject);
        }
    }

    private static Vector3 Bezier2(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * control + t * t * end;
    }
}
