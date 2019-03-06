using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private GridMovement gridMovement;
    private Vector2Int currentPosition;
    private CameraController camera;
    private GridLayer grid;


    void Start()
    {

        camera = Camera.main.GetComponent<CameraController>();
        gridMovement = GetComponent<GridMovement>();
        grid = gridMovement.grid;
        currentPosition = gridMovement.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.selected != null)
        {
            if (camera.selected.CompareTag("Enemy"))
            {
  //              particleShape.position = camera.selected.position;
  //              particleSystem.Play();
                camera.selected.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
