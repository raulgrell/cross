using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMenuPlanet : MonoBehaviour
{
    private Transform[] panels;
    public Vector3 rotationVector;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        panels = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            panels[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform t in panels)
        t.RotateAround(transform.position, rotationVector, speed);

    }
}
