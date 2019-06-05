using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatState
{
    Idle,
    Block,
    Parry,
    Threat,
    Attack,
    Hurt,
    Interacting,
}

public class GridCombat : MonoBehaviour
{
    public UnitAttack meleeAttack;
    public UnitAttack rangedAttack;
    public UnitAttack specialAttack;
    public GameObject[] Weapon;
    public MeleeIcon icon;

    private CombatState state;
    private Transform target;
    private GridLayer grid;
    private GridUnit unit;
    private bool targeting;

    //Player Death
    public GameObject playerCorpse;
    private Vector3 cameraOrigPos;
    private UnitAttack origMeleeAttack;

    private float stateTimer;
    private new PlayerAnimation playerAnimation;
    private EnemyAnimation enemyAnimation;
    private EnemySound enemySoundEffect;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public CombatState State
    {
        get => state;
        set
        {
            stateTimer = 0;
            state = value;
        }
    }

    public GridLayer Grid => grid;
    public GridUnit Unit => unit;

    void Start()
    {
        unit = GetComponent<GridUnit>();
        grid = unit.Grid;
        if (transform.CompareTag("Player"))
        {
            playerAnimation = GetComponent<PlayerAnimation>();
            cameraOrigPos = Camera.main.transform.position;
            origMeleeAttack = meleeAttack;
        }
        else if (transform.CompareTag("Enemy"))
        {
            enemySoundEffect = GetComponent<EnemySound>();
            enemyAnimation = GetComponent<EnemyAnimation>();
        }

        state = CombatState.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case CombatState.Idle:
                break;
            case CombatState.Block:
                if (stateTimer > 0.5f)
                    State = CombatState.Idle;
                break;
            case CombatState.Parry:
                if (stateTimer > 0.2f)
                    State = CombatState.Idle;
                break;
            case CombatState.Threat:
                if (stateTimer > 0.3f)
                    State = CombatState.Idle;
                break;
            case CombatState.Attack:
                if (stateTimer > 0.5f)
                    State = CombatState.Idle;
                break;
            case CombatState.Hurt:
                if (stateTimer > 0.2f)
                    State = CombatState.Idle;
                break;
            case CombatState.Interacting:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        stateTimer += Time.deltaTime;
    }

    public void Attack(UnitAttack attack)
    {
        state = CombatState.Attack;
        
        StartCoroutine(attack.Attack(this));

        if (unit.CompareTag("Player"))
        {
            playerAnimation.AttackAnimation();
        }
        else if (unit.CompareTag("Enemy"))
        {
            enemySoundEffect.onAttack();
            enemyAnimation.AttackAnimation();
        }

        stateTimer = 0;
    }
    
    public void DamageTarget(GridNode node, Target target)
    {
        if (node.unit == null || node.unit == unit)
            return;

        if (node.unit.transform.CompareTag("Player"))
        {
            CombatHealth health = node.unit.gameObject.GetComponent<CombatHealth>();
            GridCombat playerCombat = node.unit.GetComponent<GridCombat>();
            if (playerCombat.state != CombatState.Hurt && playerCombat.state != CombatState.Block)
            {
                if (target.effect == EffectType.Damage || target.effect == EffectType.Both)
                {
                    if (health.Damage(target.damage))
                    {
                        Vector3 playerPos = playerCombat.transform.position;
                        Instantiate(playerCombat.playerCorpse,
                            new Vector3(playerPos.x,
                                playerCombat.playerCorpse.transform.position.y,
                                playerPos.z),
                            playerCombat.playerCorpse.transform.rotation, null);
                        Camera.main.transform.position = playerCombat.cameraOrigPos;
                        node.unit.Respawn();
                        health.healthUI.ResetHearts();
                        health.health = 5;
                        playerCombat.meleeAttack = playerCombat.origMeleeAttack;
                        foreach (GameObject w in playerCombat.Weapon)
                        {
                            w.SetActive(false);
                        }

                        node.unit = null;
                        return;
                    }
                    
                    playerCombat.playerAnimation.HurtAnimation();
                    playerCombat.state = CombatState.Hurt;
                }
                if (target.effect == EffectType.Both)
                {
                     node.unit.MoveToPosition(node.unit.Position + unit.Forward * target.knockback);
                }
            }
            else if(playerCombat.state == CombatState.Block)
            {
                playerCombat.icon.onChangeWeapon(meleeAttack);
                UnitAttack attack = meleeAttack;
                meleeAttack = playerCombat.meleeAttack;
                playerCombat.meleeAttack = attack;
                Weapon[0].SetActive(false);
                foreach (GameObject w in playerCombat.Weapon)
                    w.SetActive(w.name == Weapon[0].name);
                enemyAnimation.ChangeAnimationState();
            }
        }
        else if (node.unit.transform.CompareTag("Enemy"))
        {
            CombatHealth health = node.unit.gameObject.GetComponent<CombatHealth>();
            if (target.effect == EffectType.Damage || target.effect == EffectType.Both)
            {
                node.unit.transform.GetComponent<GridCombat>().enemySoundEffect.onHurt();
                node.unit.transform.GetComponent<EnemyAnimation>().HurtAnimation();
                if (health.Damage(target.damage))
                {
                    Destroy(node.unit.gameObject);
                    node.unit = null;
                    return;
                }
            }
            if (target.effect == EffectType.Both)
            {
                node.unit.MoveToPosition(node.unit.Position + unit.Forward * target.knockback);
            }
        }
        else if (node.unit.transform.CompareTag("Interactable"))
        {
            InteractableObj obj = node.unit.GetComponent<InteractableObj>();
            if (target.effect == EffectType.Both || target.effect == EffectType.Condition)
            {
                node.unit.MoveToPosition(node.unit.Position + unit.Forward * target.knockback);
                grid.Nodes[obj.Position.y, obj.Position.x].unit = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!target) return;
        var targetPos = target.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(targetPos, Vector3.one);
        Gizmos.DrawLine(transform.position, targetPos);
    }

    public void Block()
    {
        State = CombatState.Block;
    }

    public void Parry()
    {
        State = CombatState.Parry;
    }


}