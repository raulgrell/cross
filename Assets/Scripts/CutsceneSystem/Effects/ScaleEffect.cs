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
    [SerializeField]
    private bool rectTransform;

    private Vector3 currentScale;
    private Vector3 currentSize;

    public override void Apply(CutscenePanel panel, GameObject gameObject)
    {
        if (!rectTransform)
        {
            if (scaleSpeed != 0)
            {
                if (scale.x >= currentScale.x && scale.y >= currentScale.y)
                {
                    currentScale.x += scaleSpeed;
                    currentScale.y += scaleSpeed;
                    gameObject.transform.localScale = currentScale;

                }
            }
        }
        else
        {
            if (scaleSpeed != 0)
            {
                if (scale.x > currentSize.x)
                    currentSize.x += scaleSpeed;
                if(scale.y > currentSize.y)
                    currentSize.y += scaleSpeed;
                    gameObject.GetComponent<RectTransform>().sizeDelta = currentSize;

            }
        }
    }

    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        if (!rectTransform)
        {
            gameObject.transform.localScale = intialScaleImage;
            currentScale = intialScaleImage;
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(intialScaleImage.x, intialScaleImage.y);
            currentSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        }
    }
}
