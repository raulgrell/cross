using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Effects/Scale")]
public class ScaleEffect : Effect
{
    [SerializeField]
    private Vector3 scaleImage;

    public override void Apply(CutscenePanel panel, GameObject image)
    {
    }

    public override void Setup(CutscenePanel panel, GameObject image)
    {
        image.transform.localScale = scaleImage;
    }
}
