using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public enum GridUnitState
{
    Idle,
    Moving,
    Attacking,
}

public class GridUnit : MonoBehaviour
{
    public Vector2Int position;
    public GridLayer grid;

    internal GridUnitState state;

    private Vector2Int prevPosition;
    
    public Vector2Int forward = Vector2Int.up;
    public Vector2Int right => new Vector2Int(forward.y, -forward.x);

    void Start()
    {
        state = GridUnitState.Idle;
        prevPosition = position;

        if (grid.IsWalkable(position.x, position.y))
            MoveToPosition(position);
    }

    public void LookAt(Vector2Int target)
    {
        forward = DirectionTo(target);
        var world = grid.CellToWorld(target);
        world.y = transform.position.y;
        transform.LookAt(world);
    }

    public void Move(Vector2Int moveDir)
    {
        var target = position + moveDir;
        LookAt(target);
        MoveToPosition(target);
    }

    public Vector2Int DirectionTo(Vector2Int target)
    {
        var direction = target - position;
        var absDir = new Vector2Int(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        direction.Clamp(new Vector2Int(-1, -1), new Vector2Int(1, 1));
        return absDir.x > absDir.y 
            ? new Vector2Int(direction.x, 0) 
            : new Vector2Int(0, direction.y);
    }

    public void MoveTowards(Vector2Int newPos)
    {
        var direction = DirectionTo(newPos);
        Move(direction);
    }

    public void MoveToPosition(Vector2Int newPos)
    {
        if (newPos.x < 0 || newPos.x >= grid.numCols ||
            newPos.y < 0 || newPos.y >= grid.numRows)
            return;

        if (!grid.IsWalkable(newPos.x, newPos.y))
            return;

        prevPosition = position;
        position = newPos;
        state = GridUnitState.Moving;
        grid.nodes[newPos.y, newPos.x].unit = this;
    }

    public void MoveToWorldPosition(Vector3 newWorldPos)
    {
        var newPos = grid.WorldToCell(newWorldPos);
        MoveToPosition(newPos);
    }

    void Update()
    {
        var local = transform.localEulerAngles;

        switch (state)
        {
            case GridUnitState.Idle:
                transform.localEulerAngles = new Vector3(0, local.y, 0);
                break;
            case GridUnitState.Moving:
                var unitPosition = transform.position;
                var cellPosition = grid.CellToWorld(position);
                cellPosition.y = unitPosition.y;
                transform.position = Vector3.MoveTowards(unitPosition, cellPosition, 16 * Time.deltaTime);
                if (Vector3.Distance(transform.position, cellPosition) < 0.1f)
                {
                    state = GridUnitState.Idle;
                    grid.nodes[prevPosition.y, prevPosition.x].unit = null;
                }
                transform.localEulerAngles = new Vector3(5, local.y, 0);
                break;
            case GridUnitState.Attacking:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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