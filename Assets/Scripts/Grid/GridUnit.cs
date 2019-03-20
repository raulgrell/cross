using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

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
        // TODO: Actually fix this
        if (newPos.x < 0 || newPos.x >= grid.numCols || newPos.y < 0 || newPos.y >= grid.numCols)
            return;

        // Notify grid of the movement
        grid.nodes[position.y, position.x].unit = null;
        grid.nodes[newPos.y, newPos.x].unit = this;
        position = newPos;
    }

    void Update()
    {
        // Don't attempt to move if there is no input
        if (input != Vector2Int.zero)
        {
            var newPos = position + input;
            // TODO: work out where to account for the bounds
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

    [Button("Apply")]
    private void Apply()
    {
        var unitPosition = transform.position;
        var cellPosition = grid.CellToWorld(position);
        cellPosition.y = unitPosition.y;
        transform.position = cellPosition;
    }
}