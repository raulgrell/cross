using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Image")]
public class CutsceneImage : ScriptableObject
{
    public Sprite image;
    public Effect[] effects;
}