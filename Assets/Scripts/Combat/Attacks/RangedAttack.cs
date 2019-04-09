using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged")]
public class RangedAttack : UnitAttack
{
    [Range(0, 8)]
    public int currentFrame;
    public List<TargetFrame> frames;
}


[System.Serializable]
public struct TargetFrame
{
    public Vector2Int position;
    public int frame;
}
