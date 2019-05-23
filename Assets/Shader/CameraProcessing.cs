using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraProcessing : MonoBehaviour
{
    public Material imageEffect;
    private double blurAmount = 0;
    private double timer = 0;
    internal bool active;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, imageEffect);
    }

    private void Update()
    {
        if (active)
        {
            DoHurtAnimation();
        }
    }
    bool reversed;
    private void DoHurtAnimation()
    {
 
        timer += 0.001f;

        if (timer <= 0.1)
        {
            if (blurAmount >= 0.005 && !reversed)
                reversed = true;
            else if (blurAmount == 0 && reversed)
            {
                reversed = false;
                active = false;
                timer = 0;
            }
            if (active) { 
            if (!reversed)
            {
                blurAmount += 0.0005;
            }
            else
            {
                blurAmount -= 0.0005;
            }
            }
            else
               blurAmount = 0;

            UpdateBlur();
        }
        else
            return;
    }
    private void UpdateBlur()
    {
       imageEffect.SetFloat("_BlurAmount", (float)blurAmount);
    }
}
