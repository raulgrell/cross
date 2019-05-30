using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee")]
public class MeleeAttack : UnitAttack
{
    public int melee;
    
    public override IEnumerator Attack(GridCombat combat)
    {
        var threatened = GetThreatened(combat.Unit);

        for (int i = 0; i < threatened.Length; i++)
        {
            Target hit = threatened[i];
            Vector2Int gridPosition = hit.position;
            
            if (!combat.Grid.InBounds(gridPosition.x, gridPosition.y))
                continue;
            
            GridNode node = combat.Grid.Nodes[gridPosition.y, gridPosition.x];
            Vector3 worldPosition = combat.Grid.CellToWorld(node.gridPosition);
            worldPosition.y = combat.Unit.transform.position.y;
            Instantiate(attackPrefab, worldPosition, attackPrefab.transform.rotation, null);
            combat.DamageTarget(node, threatened[i]);
        }
        yield break;
    }
}