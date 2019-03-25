using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cutscene/Panel")]
public class PanelData : ScriptableObject
{
    [SerializeField] private float duration;
    [SerializeField] private float startTime;
    [SerializeField] private CutsceneImage[] images;
    [SerializeField] private CutsceneText[] texts;
    [SerializeField] private CutsceneImage backgroundImage;
    [SerializeField] private CutsceneMask mask;
    [SerializeField] private Vector3 panelScale;

    public CutsceneMask Mask => mask;
    public Vector3 PanelScale => panelScale;
    public CutsceneImage BackgroundImage => backgroundImage;
    public CutsceneImage[] Images => images;
    public CutsceneText[] Texts => texts;
    public float Duration => duration;
    public float StartTime => startTime;
    public float EndTime => startTime + duration;
}