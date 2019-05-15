using System;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged,
    Special
}

public enum EffectType
{
    None,
    Damage,
    Condition,
}

public abstract class UnitAttack : ScriptableObject
{
    public GameObject targetPrefab;
    public GameObject attackPrefab;
    public List<Target> targets;

    [Range(0, 3)] public int spread = 1;

    [Range(1, 6)] public int range = 1;

    public Target[] GetThreatened(GridUnit unit)
    {
        var t = new Target[targets.Count];
        for (int i = 0; i < targets.Count; i++)
        {
            var p = targets[i];
            t[i] = new Target
            {
                position = unit.Position + unit.Right * p.position.x + unit.Forward * p.position.y,
                effect = p.effect,
                damage = p.damage
            };
        }

        return t;
    }
}

[System.Serializable]
public class Target
{
    public Vector2Int position;
    public EffectType effect;
    public int damage;
}

