using System;
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
    public MeleeIcon icon;

    private CombatState state;
    private Transform target;
    private GridLayer grid;
    private GridUnit unit;
    private bool targeting;

    //Player Death
    private Vector3 playerOrigPos;
    private Vector3 cameraOrigPos;
    private Vector2Int gridOrigPos;

    private float stateTimer;
    private new PlayerAnimation animation;
    private EnemyAniamtion enemyAnimation;
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

    void Start()
    {
        cameraOrigPos = Camera.main.transform.position;
        unit = GetComponent<GridUnit>();
        grid = unit.grid;
        if (transform.CompareTag("Player"))
        {
            animation = GetComponent<PlayerAnimation>();
            gridOrigPos = unit.Position;
            playerOrigPos = transform.position;
        }
        else if (transform.CompareTag("Enemy"))
        {
            enemySoundEffect = GetComponent<EnemySound>();
            enemyAnimation = GetComponent<EnemyAniamtion>();
        }
        grid = unit.grid;
        state = CombatState.Idle;
    }

    void Update()
    {
        
        switch (state)
        {
            case CombatState.Idle:
                break;
            case CombatState.Block:
                if (stateTimer > 1f)
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
                break;
            case CombatState.Interacting:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        stateTimer += Time.deltaTime;
    }
    private UnitAttack currentAttack;

    public void Attack(UnitAttack attack)
    {
        state = CombatState.Attack;
        currentAttack = attack;

        if (unit.CompareTag("Player"))
        {
            if (attack.name == "BasicAttack")
                animation.BasicAttackAnimation();
            else
                animation.AttackAnimation();
        }
        else if (unit.CompareTag("Enemy"))
        {
            enemySoundEffect.onAttack();
            enemyAnimation.AttackAnimation();
        }
        stateTimer = 0;
        

    }

    public void DamageTile()
    {
        var threatened = currentAttack.GetThreatened(unit);

        for (int i = 0; i < threatened.Length; i++)
        {
            Target hit = threatened[i];
            Vector2Int gridPosition = hit.position;

            if (grid.InBounds(gridPosition.x, gridPosition.y))
            {
                GridNode node = grid.Nodes[gridPosition.y, gridPosition.x];
                Vector3 worldPosition = grid.CellToWorld(node.gridPosition);
                worldPosition.y = transform.position.y;

                Instantiate(meleeAttack.attackPrefab, worldPosition, meleeAttack.attackPrefab.transform.rotation, null);

                if (node.unit != null && node.unit != unit)
                {
                    CombatHealth health = node.unit.gameObject.GetComponent<CombatHealth>();
                    if (node.unit.transform.CompareTag("Player"))
                    {
                        GridCombat playerCombat = node.unit.GetComponent<GridCombat>();
                        if (playerCombat.state != CombatState.Hurt && playerCombat.state != CombatState.Block)
                        {
                            if (health.Damage(1))
                            {
                                Camera.main.transform.position = playerCombat.cameraOrigPos;
                                node.unit.position = playerCombat.gridOrigPos;
                                node.unit.transform.position = playerCombat.playerOrigPos;
                                health.healthUI.ResetHearts();
                                health.health = 5;
                            }
                            else
                                playerCombat.animation.HurtAnimation();

                        }
                        else if (playerCombat.state == CombatState.Block)
                        {
                            playerCombat.icon.onChangeWeapon(meleeAttack);
                            playerCombat.meleeAttack = meleeAttack;
                            //if(node.unit.transform.CompareTag("Enemy"))
                        }
                    }
                    else if(node.unit.transform.CompareTag("Enemy"))
                    {
                        node.unit.transform.GetComponent<GridCombat>().enemySoundEffect.onHurt();
                        node.unit.transform.GetComponent<EnemyAniamtion>().HurtAnimation();
                        if(health.Damage(1))
                        Destroy(node.unit.gameObject);
                    }
                }
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