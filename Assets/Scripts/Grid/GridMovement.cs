using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class GridMovement : MonoBehaviour
{
    public Vector2Int position;
    public GridLayer grid;
    public bool playerBool;
    private GridElement element;

    void Start()
    {
        element = GetComponent<GridElement>();
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
        grid.nodes[position.y, position.x].occupant = null;
        grid.nodes[newPos.y, newPos.x].occupant = element;
        position = newPos;
    }

    void Update()
    {
        if (playerBool)
        {
            // Collect input
            var input = Vector2Int.zero;
            if (Input.GetKeyDown(KeyCode.A)) input.x -= 1;
            if (Input.GetKeyDown(KeyCode.D)) input.x += 1;
            if (Input.GetKeyDown(KeyCode.S)) input.y -= 1;
            if (Input.GetKeyDown(KeyCode.W)) input.y += 1;

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
            }
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

        Gizmos.DrawSphere(grid.CellToWorld(position), 1);
    }
}