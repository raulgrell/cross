using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

[RequireComponent(typeof(GridMovement))]
public class EnemyController : MonoBehaviour
{
    private GridMovement movement;
    private Vector2Int nextPosition;
    private float timer;
    private float waitTime;
    
    void Start()
    {
        movement = GetComponent<GridMovement>();
        waitTime = Random.Range(1, 2);
        CalculateNextPosition();
    }

    void Update()
    {
        if (timer > waitTime)
        {
            movement.MoveToPosition(nextPosition);
            CalculateNextPosition();
            waitTime = Random.Range(1, 2);
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void CalculateNextPosition()
    {
        nextPosition = movement.position + new Vector2Int(Random.Range(-1,2), Random.Range(-1,2));
        nextPosition.x = Mathf.Clamp(nextPosition.x, 0, movement.grid.numCols - 1);
        nextPosition.y = Mathf.Clamp(nextPosition.y, 0, movement.grid.numRows - 1);
    }

    private void OnValidate()
    {
        movement = GetComponent<GridMovement>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(movement.grid.CellToWorld(nextPosition), Vector3.one);
    }
}
