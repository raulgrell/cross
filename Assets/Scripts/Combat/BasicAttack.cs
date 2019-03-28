using System;
using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged,
    Special
}

public class BasicAttack
{
    public Vector2Int centerPoint;
    public int[] positions;
    public float speed;

    public BasicAttack(Vector2Int center, int[] pos, float speed)
    {
        positions = pos;
        this.speed = speed;
        centerPoint = center;
    }

    public Vector2Int[] doAttack(float rotation, int range, AttackType type)
    {
        Vector2Int direction = centerPoint;
        
        // TODO: Fix range calculation. At the moment this is wrong
        var neighbours = new Vector2Int[8];
        neighbours[0] = new Vector2Int(direction.x + 0, direction.y + range);
        neighbours[1] = new Vector2Int(direction.x + range, direction.y + range);
        neighbours[2] = new Vector2Int(direction.x + range, direction.y + 0);
        neighbours[3] = new Vector2Int(direction.x + range, direction.y - range);
        neighbours[4] = new Vector2Int(direction.x + 0, direction.y - range);
        neighbours[5] = new Vector2Int(direction.x - range, direction.y - range);
        neighbours[6] = new Vector2Int(direction.x - range, direction.y + 0);
        neighbours[7] = new Vector2Int(direction.x - range, direction.y + range);

        var sectors = new[]
        {
            new[] {7, 0, 1},
            new[] {0, 1, 2},
            new[] {1, 2, 3},
            new[] {2, 3, 4},
            new[] {3, 4, 5},
            new[] {4, 5, 6},
            new[] {5, 6, 7},
            new[] {6, 7, 0},
        };

        int sector = (int) ((rotation + 22.5f) / 45f) % 8;

        Vector2Int[] threatened;

        switch (type)
        {
            case AttackType.Melee:
                threatened = new[]
                {
                    neighbours[sectors[sector][positions[0]]],
                    neighbours[sectors[sector][positions[1]]],
                    neighbours[sectors[sector][positions[2]]]
                };
                break;
            case AttackType.Ranged:
            {
                var attackRange = 3;
                threatened = new Vector2Int[attackRange];
                for (int i = 0; i < attackRange; i++)
                {
                    threatened[i] = neighbours[sectors[sector][positions[0]]] +
                                    (neighbours[sectors[sector][positions[0]]] - direction) * i;
                }
                break;
            }
            case AttackType.Special:
                threatened = neighbours;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return threatened;
    }
}