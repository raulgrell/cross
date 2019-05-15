﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniamtion : MonoBehaviour
{
    internal Animator enemyAnimator;
    private GridUnit input;
    private GridCombat combatInput;
    private bool playing;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<GridUnit>();
        combatInput = GetComponent<GridCombat>();
        enemyAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        WalkingAnimation();
    }
    void WalkingAnimation()
    {
        if (input.State == GridUnitState.Moving)
        {
            enemyAnimator.SetBool("Walking", true);
            playing = true;
            timer = 0;
        }
        if (playing)
            timer += 0.1f;

        if (timer > 1.5f)
        {
            enemyAnimator.SetBool("Walking", false);
            playing = false;
            timer = 0;
        }
    }
    public void HurtAnimation()
    {
        combatInput.State = CombatState.Hurt;
        enemyAnimator.Play("Hurt");
    }
    public void EndHurtAnimation()
    {
        combatInput.State = CombatState.Idle;        
    }

    public void AttackAnimation()
    {
        enemyAnimator.Play("Attack");

    }
}
