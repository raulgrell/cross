using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public AnimationClip[] DisarmIdle;

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
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = enemyAnimator.runtimeAnimatorController;

        foreach (AnimationClip clipOverride in DisarmIdle)
        {
            overrideController["idle"] = clipOverride;
        }

       walkingAnimation = "DisarmedWalking";
       enemyAnimator.runtimeAnimatorController = overrideController;
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
        enemyAnimator.Play(combatInput.meleeAttack.name);
    }
}
