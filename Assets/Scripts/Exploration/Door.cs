using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    private Animator animator;
    private static readonly int isOpen = Animator.StringToHash("IsOpen");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        open = true;
        animator.SetBool(isOpen, true);
    }

    public void Close()
    {
        open = false;
        animator.SetBool(isOpen, false);
    }
}
