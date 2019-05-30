using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraTexture : MonoBehaviour
{
    public Camera securityCamera;
    public RenderTexture texture;

    void Awake()
    {
        securityCamera.targetTexture = texture;
    }
}
