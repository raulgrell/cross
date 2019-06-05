using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CombatProjectile : MonoBehaviour
{
    [Range(0.1f, 10f)] public float factor = 1;
    private CombatLauncher launcher;
    private Vector3 start;
    private Vector3 end;
    private float lifetime;

    public static CombatProjectile Spawn(GameObject prefab, Vector3 start, Vector3 end, CombatLauncher launcher)
    {
        var gameObject = Instantiate(prefab, start, Quaternion.identity);
        var projectile = gameObject.GetComponent<CombatProjectile>();
        projectile.start = start;
        projectile.end = end;
        projectile.launcher = launcher;
        return projectile;
    }

    private void Update()
    {
        if (lifetime < 1)
        {
            var midpoint = new Vector3((start.x + end.x)/ 2, start.y * 2, (start.z + end.z)/2);
            transform.position = Bezier2(start, midpoint, end, lifetime);
            lifetime += Time.deltaTime * factor;
        }
        else
        {
            launcher.Combat.Attack(launcher.Combat.meleeAttack);
            Destroy(gameObject);
        }
    }

    private static Vector3 Bezier2(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * control + t * t * end;
    }
}
