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


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, imageEffect);
    }
    public void DoHurtAnimatio()
    {
        timer += 0.001f;

        if (timer <= 0.1)
        {
            if (blurAmount < 0.05)
                blurAmount += 0.001;
            else
                blurAmount -= 0.001;

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
