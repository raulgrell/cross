﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuCamera : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed, 0));
    }
}
