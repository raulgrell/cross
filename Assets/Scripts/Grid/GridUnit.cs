using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridUnit : MonoBehaviour
{
    public Vector2Int input;
    public Vector2Int position;
    public GridLayer grid;

    void Start()
    {
        if (grid.CellIsWalkable(position))
        {
            MoveToPosition(position);
        }
    }

    public void MoveToPosition(Vector2Int newPos)
    {
        if (newPos.x < 0 || newPos.x >= grid.numCols ||
            newPos.y < 0 || newPos.y >= grid.numRows)
            return;

        // Notify grid of the movement
        grid.nodes[position.y, position.x].unit = null;
        grid.nodes[newPos.y, newPos.x].unit = this;
        position = newPos;
    }

    public void MoveToWorldPosition(Vector3 newWorldPos)
    {
        var newPos = grid.WorldToCell(newWorldPos);
        MoveToPosition(newPos);
    }

    void Update()
    {
        if (input != Vector2Int.zero)
        {
            var newPos = position + input;
            newPos.x = Mathf.Clamp(newPos.x, 0, grid.numCols - 1);
            newPos.y = Mathf.Clamp(newPos.y, 0, grid.numRows - 1);

            if (grid.CellIsWalkable(newPos))
            {
                MoveToPosition(newPos);
            }
            
            input = Vector2Int.zero;
        }

        // Update the unit position according to the destination
        var unitPosition = transform.position;
        var cellPosition = grid.CellToWorld(position);
        cellPosition.y = unitPosition.y;

        transform.position = Vector3.MoveTowards(unitPosition, cellPosition, 16 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
            return;

        Gizmos.DrawWireSphere(grid.CellToWorld(position), 1);
    }

    private void Apply()
    {
        var unitPosition = transform.position;
        var cellPosition = grid.CellToWorld(position);
        cellPosition.y = unitPosition.y;
        transform.position = cellPosition;
    }
}