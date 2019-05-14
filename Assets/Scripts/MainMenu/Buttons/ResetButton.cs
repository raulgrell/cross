using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private Transform[] childs;

    private void Awake()
    {
        childs = new Transform[transform.childCount];
        for(int i = 0; i < childs.Length; i++) 
        {
            childs[i] = transform.GetChild(0).GetChild(i);
        }
    }

    private void OnEnable()
    {
        foreach(Transform t in childs)
        {
            t.GetComponent<Animator>().Play("Idle");
        }
    }
}
