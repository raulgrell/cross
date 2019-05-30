using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    internal Animator enemyAnimator;
    private GridUnit input;
    private GridCombat combatInput;
    private string walkingAnimation = "Walking";
    private bool playing;
    private float timer;

    void Start()
    {
        input = GetComponent<GridUnit>();
        combatInput = GetComponent<GridCombat>();
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        WalkingAnimation();
    }

    void WalkingAnimation()
    {
        if (input.State == GridUnitState.Moving)
        {
            enemyAnimator.SetBool(walkingAnimation, true);
            playing = true;
            timer = 0;
        }
        if (playing)
            timer += 0.1f;

        if (timer > 1.5f)
        {
            enemyAnimator.SetBool(walkingAnimation, false);
            playing = false;
            timer = 0;
        }
    }

    public void ChangeAnimationState()
    {
        enemyAnimator.SetFloat("IdleBlend", 1);
        enemyAnimator.SetFloat("WalkingBlend", 1);

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
        if(combatInput.meleeAttack.name == "BasicAttack")
        {
            enemyAnimator.SetFloat("AttackBlend", 1);
        }
        else
            enemyAnimator.SetFloat("AttackBlend", 0);

        enemyAnimator.Play("AttackBlendTree");
    }
}
