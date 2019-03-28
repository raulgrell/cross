using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class GridInput : MonoBehaviour
{
    private GridUnit movement;
    private GridCombat combat;
    
    void Start()
    {
        movement = GetComponent<GridUnit>();
        combat = GetComponent<GridCombat>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) movement.input.x -= 1;
        if (Input.GetKeyDown(KeyCode.D)) movement.input.x += 1;
        if (Input.GetKeyDown(KeyCode.S)) movement.input.y -= 1;
        if (Input.GetKeyDown(KeyCode.W)) movement.input.y += 1;
        
    }
}
