using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/SpriteMask")]
public class CutsceneMask : ScriptableObject
{
    public Sprite image;
    public Effect[] effects;
    public int order1, order2;
    public Vector2 size;
}
