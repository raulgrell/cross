using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    internal Animator playerAnimator;
    private new Camera camera;
    private GridUnit input;
    private GridCombat combatInput;
    private bool playing;
    private float timer;

    void Start()
    {
        camera = Camera.main;
        input = GetComponent<GridUnit>();
        combatInput = GetComponent<GridCombat>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        WalkingAnimation();
    }

    public void HurtAnimation()
    {
        camera.GetComponent<CameraProcessing>().active = true;
    }

    public void EndAttackAnimation()
    {

    }

    public void Dance()
    {
        playerAnimator.SetBool("Dancing", true);
    }
    
    public void StopDancing()
    {
        playerAnimator.SetBool("Dancing", false);
    }
    
    public void BlockAnimation()
    {
        playerAnimator.Play("Blocking");
    }

    public void AttackAnimation()
    {
        if(combatInput.meleeAttack.name == "BasicAttack")
        {
            playerAnimator.SetFloat("AttackXBlend", 0);
            playerAnimator.SetFloat("AttackYBlend", -1);

        }
        if(combatInput.meleeAttack.name == "SlashAttack")
        {
            playerAnimator.SetFloat("AttackXBlend", 0);
            playerAnimator.SetFloat("AttackYBlend", 1);
        }
        playerAnimator.Play("AttackBlendTree");
    }


    void WalkingAnimation()
    {
        if (input.State == GridUnitState.Moving)
        {
            playerAnimator.SetBool("Walking", true);
            playing = true;
            timer = 0;
        }
        
        if (playing)
            timer += 0.1f;

        if (timer > 2f)
        {
            playerAnimator.SetBool("Walking", false);
            playing = false;
            timer = 0;
        }
    }

    internal void ChangeAniamtionState()
    {
        playerAnimator.SetFloat("IdleBlend", 1);
        playerAnimator.SetFloat("MoveBlend", 1);
    }
}
