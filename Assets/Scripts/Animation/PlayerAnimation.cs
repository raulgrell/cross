using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public CameraProcessing cameraAnimator;

    internal Animator playerAnimator;
    private GridUnit input;
    private GridCombat combatInput;
    private bool playing;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<GridUnit>();
        combatInput = GetComponent<GridCombat>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkingAnimation();
    }

    public void HurtAnimation()
    {
        cameraAnimator.DoHurtAnimatio();
    }
    //    combatInput.State = CombatState.Hurt;
    //}
    //public void EndHurtAnimation()
    //{
    //    combatInput.State = CombatState.Idle;
    //}
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
        playerAnimator.Play("Attack3");
    }

    public void BasicAttackAnimation()
    {
        playerAnimator.Play("Attack2");
    }
    void WalkingAnimation()
    {
        if (input.state == GridUnitState.Moving)
        {
            playerAnimator.SetBool("Walking", true);
            playing = true;
            timer = 0;
        }
        if (playing)
            timer += 0.1f;

        if (timer > 1.5f)
        {
            playerAnimator.SetBool("Walking", false);
            playing = false;
            timer = 0;
        }
    }

}
