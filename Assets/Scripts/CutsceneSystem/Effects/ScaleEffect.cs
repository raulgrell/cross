using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Effects/Scale")]
public class ScaleEffect : Effect
{
    [SerializeField]
    private Vector3 intialScaleImage;
    [SerializeField]
    private float scaleSpeed;
    [SerializeField]
    private Vector3 scale;

    private Vector3 currentScale;

    public override void Apply(CutscenePanel panel, GameObject gameObject)
    {
        if (scaleSpeed != 0)
        {
            if(scale.x >= currentScale.x && scale.y >= currentScale.y)
            {
                currentScale.x += scaleSpeed;
                currentScale.y += scaleSpeed;
                gameObject.transform.localScale = currentScale;

            }
        }
    }

    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        gameObject.transform.localScale = intialScaleImage;
        currentScale = intialScaleImage;
    }
}
