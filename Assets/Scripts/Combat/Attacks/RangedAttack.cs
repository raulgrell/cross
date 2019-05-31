using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged")]
public class RangedAttack : UnitAttack
{
    public float frameTime = 0.1f;
        
    [Range(0, 8)]
    public int currentFrame;

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