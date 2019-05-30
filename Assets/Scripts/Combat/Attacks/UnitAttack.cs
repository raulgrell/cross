using System;
using System.Collections;
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
    Both,
}

public abstract class UnitAttack : ScriptableObject
{
    public GameObject targetPrefab;
    public GameObject attackPrefab;
    public List<Target> frames;

    [Range(0, 3)] public int spread = 1;
    [Range(1, 6)] public int range = 1;
    
    private void OnEnable()
    {
        frames.Sort((a, b) => a.frame < b.frame);
    }

    public Target[] GetRange(GridUnit unit)
    {
        var t = new Target[frames.Count];
        for (int i = 0; i < frames.Count; i++)
        {
            var p = frames[i];
            t[i] = new Target
            {
                position = unit.Position + unit.Right * p.position.x + unit.Forward * p.position.y,
                effect = p.effect,
                damage = p.damage
            };
        }

        return t;
    }
    
    public Target[] GetTargets(GridUnit unit)
    {
        var t = new Target[frames.Count];
        for (int i = 0; i < frames.Count; i++)
        {
            var p = frames[i];
            t[i] = new Target
            {
                position = unit.Position + unit.Right * p.position.x + unit.Forward * p.position.y,
                effect = p.effect,
                damage = p.damage
            };
        }

        return t;
    }
    
    public Target[] GetThreatened(GridUnit unit)
    {
        var t = new Target[frames.Count];
        for (int i = 0; i < frames.Count; i++)
        {
            var p = frames[i];
            t[i] = new Target
            {
                position = unit.Position + unit.Right * p.position.x + unit.Forward * p.position.y,
                effect = p.effect,
                damage = p.damage
            };
        }

        return t;
    }

    public abstract IEnumerator Attack(GridCombat combat);
}

[System.Serializable]
public struct Target
{
    public Vector2Int position;
    public EffectType effect;
    public int damage;
    public int knockback;
    public int frame;
}
