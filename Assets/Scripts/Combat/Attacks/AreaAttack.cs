using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Area")]
public class AreaAttack : UnitAttack
{
    [Range(0, 8)]
    public int currentFrame;

    public float frameTime = 0.1f;
    public GridNode target;
    
    private void OnEnable()
    {
        if (frames != null) frames = new List<Target>();
        frames.Sort((a, b) => a.frame < b.frame);
    }

    public void SetTarget(GridNode node)
    {
        target = node;
    }

    public override Target[] GetThreatened(GridUnit unit)
    {
        var t = new Target[frames.Count];
        for (int i = 0; i < frames.Count; i++)
        {
            var p = frames[i];
            t[i] = new Target
            {
                position = target.gridPosition + p.position,
                effect = p.effect,
                damage = p.damage,
                knockback = p.knockback
            };
        }

        return t;
    }

    public override IEnumerator Attack(GridCombat combat)
    {
        var threatened = GetThreatened(combat.Unit);
        
        int runningFrame = 0;
        foreach (Target attackFrame in threatened)
        {
            if (attackFrame.frame <= runningFrame)
            {
                if (!combat.Grid.InBounds(attackFrame.position.x, attackFrame.position.y))
                    continue;
            
                GridNode node = combat.Grid.Nodes[attackFrame.position.y, attackFrame.position.x];
                Vector3 worldPosition = combat.Grid.CellToWorld(node.gridPosition);
                worldPosition.y = combat.Unit.transform.position.y;
                Instantiate(attackPrefab, worldPosition, attackPrefab.transform.rotation, null);
                combat.DamageTarget(node, attackFrame);
                yield return new WaitForSeconds(frameTime);
            }
            else
            {
                runningFrame += 1;
            }
        }
    }
}