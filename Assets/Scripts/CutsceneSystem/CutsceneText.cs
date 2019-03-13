using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Text")]
public class CutsceneText : ScriptableObject
{
    public string text;
    public Color background;
    public Color foreground;
    public Effect[] effects;
    public int size;
}