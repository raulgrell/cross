using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Cutscene/Panel")]
public class PanelData : ScriptableObject
{
    [SerializeField]
    private Effect[] effects;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private CutsceneImage[] images;
    [SerializeField]
    private CutsceneText[] texts;
    
    public Effect[] Effects => effects;
    public CutsceneImage[] Images => images;
    public CutsceneText[] Texts => texts;
    public float StartTime => startTime;
    public float Duration => duration;
    public float EndTime => startTime + duration;
}
