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
    Grabbed,
    Stunned,
}

public class GridUnit : MonoBehaviour
{
    public GridLayer grid;
    public int speed = 16;

    internal Vector2Int position;
    private Vector2Int prevPosition;
    private Vector2Int forward = Vector2Int.up;
    private GridUnitState state;
    
    public GridUnitState State => state;

    public Vector2Int Position => position;
    public Vector2Int Forward => forward;
    public Vector2Int Right => new Vector2Int(forward.y, -forward.x);

    void Start()
    {
        state = GridUnitState.Idle;
        position = grid.WorldToCell(transform.position);
        prevPosition = Position;

        if (grid.IsWalkable(Position.x, Position.y))
            MoveToPosition(Position);
    }

    public void LookAt(Vector2Int target)
    {
        forward = DirectionTo(target);
        var world = grid.CellToWorld(target);
        world.y = transform.position.y;
        transform.LookAt(world);
        if (transform.CompareTag("Enemy"))
            transform.GetChild(0).GetComponent<EnemyHealthBar>().FixRotation();
    }

    public void Move(Vector2Int moveDir)
    {
        var target = Position + moveDir;
        LookAt(target);
        MoveToPosition(target);
;
    }

    public Vector2Int DirectionTo(Vector2Int target)
    {
        var direction = target - Position;
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

        prevPosition = Position;
        position = newPos;
        state = GridUnitState.Moving;
        grid.Nodes[newPos.y, newPos.x].unit = this;
    }

    public void MoveToWorldPosition(Vector3 newWorldPos)
    {
        var newPos = grid.WorldToCell(newWorldPos);
        MoveToPosition(newPos);
    }

    void Update()
    {
        switch (state)
        {
            case GridUnitState.Idle:
                break;
            case GridUnitState.Moving:
                var unitPosition = transform.position;
                var cellPosition = grid.CellToWorld(Position);
                cellPosition.y = unitPosition.y;
                transform.position = Vector3.MoveTowards(unitPosition, cellPosition, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, cellPosition) < 0.1f)
                {
                    state = GridUnitState.Idle;
                    grid.Nodes[prevPosition.y, prevPosition.x].unit = null;
                }
                break;
            case GridUnitState.Attacking:
            case GridUnitState.Grabbed:
            case GridUnitState.Stunned:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
            return;

        Gizmos.DrawWireSphere(grid.CellToWorld(Position), 1);
    }

    [Button("Sync")]
    private void Sync()
    {
        position = grid.WorldToCell(transform.position);
        prevPosition = Position;
    }

    public void SetGrabbed()
    {
        state = GridUnitState.Grabbed;
    }
    
    public void SetStunned()
    {
        state = GridUnitState.Stunned;
    }
}
