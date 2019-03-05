using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Effects/Scale")]
public class ScaleEffect : Effect
{
    [SerializeField]
    private Vector3 scaleImage;

    public override void Apply(CutscenePanel panel, GameObject gameObject)
    {
    }

    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        gameObject.transform.localScale = scaleImage;
    }
}
