﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloveTrap : MonoBehaviour
{
    public int range;
    public Vector2Int forward;
    private GridUnit target;
    private GridLayer grid;
    private bool active;
    private float timer;
    private bool readyToFire = true;
    private GridUnit unit;
    private GridCombat combat;
    private Vector2Int[] trapPosition;
    private Vector2Int currentActive;
    private Vector2Int origPosition;
    private bool deative;
    private bool damaged;
    private void Start()
    { 
        combat = GetComponent<GridCombat>();
        unit = GetComponent<GridUnit>();
        unit.Forward = forward;
        grid = FindObjectOfType<GridLayer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<GridUnit>();
        trapPosition = new Vector2Int[range];
        origPosition = grid.WorldToCell(transform.position);
        for(int i = 0; i < trapPosition.Length; i++)
        {
            trapPosition[i] = new Vector2Int(origPosition.x + (forward.x * (i + 1)), origPosition.y + (forward.y * (i + 1)));
        }
    }
    private void Update()
    {
        if(Vector2.Distance(target.Position, unit.Position) < 20 && readyToFire)
        {
            for (int i = 0; i < trapPosition.Length; i++)
            {
                if (target.Position == trapPosition[i])
                {
                    if(i > 0)
                        currentActive = trapPosition[i - 1];
                    else
                        currentActive = trapPosition[i];

                    active = true;
                    readyToFire = false;
                }
            }                 
        }
        if (active)
        {
            ActivatedTrap();
        }
        else if (deative)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.025f * range)
        {
            if (!damaged)
            {
                combat.Attack(combat.meleeAttack);
                damaged = true;
            }
            DoDamage();
        }
    }
    private void DoDamage()
    {
        unit.speed = 8;
        unit.MoveToPosition(origPosition);
        if (Vector2.Distance(grid.WorldToCell(transform.position),origPosition) < 2)
        {
            timer = 0;
            deative = false;
            readyToFire = true;
            damaged = false;
        }
    }
    
    private void ActivatedTrap()
    {
        unit.speed = 16;
        unit.MoveToPosition(currentActive);
        if(Vector3.Distance(transform.position,grid.CellToWorld(currentActive)) < 5f)
        {
            active = false;
            deative = true;
        }

    }
}
