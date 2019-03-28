using UnityEngine;

public enum GridUnitState
{
    Idle,
    Moving,
    Charging,
    Attacking,
}

public class GridUnit : MonoBehaviour
{
    public Vector2Int input;
    public Vector2Int position;
    public GridLayer grid;

    internal GridUnitState state;
    
    private Vector2Int prevPosition;

    void Start()
    {
        state = GridUnitState.Idle;
        prevPosition = position;
        
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
        
        if (!grid.CellIsWalkable(newPos))
            return;
        
        prevPosition = position;
        position = newPos;
        state = GridUnitState.Moving;
        
        // Notify grid of the movement
        grid.nodes[newPos.y, newPos.x].unit = this;
        
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

            MoveToPosition(newPos);
            input = Vector2Int.zero;

            var world = grid.CellToWorld(newPos);
            world.y = transform.position.y;
            transform.LookAt(world);
        }
        
        // Update the unit position according to the destination
        var unitPosition = transform.position;
        var cellPosition = grid.CellToWorld(position);
        cellPosition.y = unitPosition.y;

        transform.position = Vector3.MoveTowards(unitPosition, cellPosition, 16 * Time.deltaTime);

        if (Vector3.Distance(transform.position, cellPosition) < 0.1f)
        {
            state = GridUnitState.Idle;
            grid.nodes[prevPosition.y, prevPosition.x].unit = null;
        }
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