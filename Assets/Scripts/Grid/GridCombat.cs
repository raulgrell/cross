﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject specialPrefab;
    public new Camera camera;
    private GridLayer grid;
    private GridUnit gridUnit;

    private List<GridNode> neighbors;
    
    private Transform target;
    private bool targeting;

    void Start()
    {
        gridUnit = GetComponent<GridUnit>();
        grid = gridUnit.grid;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0.2f;
            targeting = true;
        }
        
        if (targeting && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            target = hitInfo.transform;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Time.timeScale = 1f;                
            targeting = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(target.position, Vector3.one);
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
