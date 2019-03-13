using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class GridInput : MonoBehaviour
{
    private GridUnit movement;
    
    void Start()
    {
        movement = GetComponent<GridUnit>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) movement.input.x -= 1;
        if (Input.GetKeyDown(KeyCode.D)) movement.input.x += 1;
        if (Input.GetKeyDown(KeyCode.S)) movement.input.y -= 1;
        if (Input.GetKeyDown(KeyCode.W)) movement.input.y += 1;
    }
}
