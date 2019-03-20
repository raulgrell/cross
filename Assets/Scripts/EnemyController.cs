using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class EnemyController : MonoBehaviour
{
    private GridUnit unit;
    private Vector2Int nextPosition;
    private float timer;
    private float waitTime;
    
    void Start()
    {
        unit = GetComponent<GridUnit>();
        waitTime = Random.Range(1f, 2f);
        CalculateNextPosition();
    }

    void Update()
    {
        if (timer > waitTime)
        {
            unit.MoveToPosition(nextPosition);
            CalculateNextPosition();
            waitTime = Random.Range(1f, 2f);
            timer = 0;
        }
        
        timer += Time.deltaTime;
    }

    private void CalculateNextPosition()
    {
        nextPosition = unit.position + new Vector2Int(Random.Range(-1,2), Random.Range(-1,2));
        nextPosition.x = Mathf.Clamp(nextPosition.x, 0, unit.grid.numCols - 1);
        nextPosition.y = Mathf.Clamp(nextPosition.y, 0, unit.grid.numRows - 1);
    }

    private void OnValidate()
    {
        // OnDrawGizmos requires reference to the unit
        unit = GetComponent<GridUnit>();
    }

    private void OnDrawGizmos()
    {
        if (unit == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(unit.grid.CellToWorld(nextPosition), Vector3.one);
    }
}
