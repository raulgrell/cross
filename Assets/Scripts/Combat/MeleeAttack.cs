using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee")]
public class MeleeAttack : Attack
{
    [SerializeField]
    Vector2Int[] grid;

    public override void Act(GridLayer grid, GridUnit unit, int sector)
    {
        throw new System.NotImplementedException();
    }
}
