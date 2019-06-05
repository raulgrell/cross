using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private static readonly int isOpen = Animator.StringToHash("IsOpen");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool(isOpen, true);
    }

    public void Close()
    {
        animator.SetBool(isOpen, false);
    }
}
