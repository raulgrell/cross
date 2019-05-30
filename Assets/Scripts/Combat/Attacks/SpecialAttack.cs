using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Special")]
public class SpecialAttack : UnitAttack
{
    public int distance;
    public override IEnumerator Attack(GridCombat combat)
    {
        throw new NotImplementedException();
    }
}