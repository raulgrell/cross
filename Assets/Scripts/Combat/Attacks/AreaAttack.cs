using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Area")]
public class AreaAttack : UnitAttack
{
    [Range(0, 8)]
    public int currentFrame;
    
    private void OnEnable()
    {
        if (frames != null) frames = new List<Target>();
        frames.Sort((a, b) => a.frame < b.frame);
    }

    public override IEnumerator Attack(GridCombat combat)
    {
        throw new NotImplementedException();
    }
}