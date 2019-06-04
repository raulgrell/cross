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
    public int speed = 16;
    private GridLayer grid;
    private Vector2Int position;
    private Vector2Int prevPosition;
    private Vector2Int initialPosition;
    private Vector2Int forward = Vector2Int.up;
    private GridUnitState state;

    public GridLayer Grid => grid;
    public Vector2Int Position => position;
    public Vector2Int Forward { get => forward; set => forward = value; }
    public Vector2Int Right => new Vector2Int(forward.y, -forward.x);
    public GridUnitState State => state;

    private void Awake()
    {
        grid = FindObjectOfType<GridLayer>();
    }

    void Start()
    {
        state = GridUnitState.Idle;
        position = grid.WorldToCell(transform.position);
        prevPosition = position;
        initialPosition = position;
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

    public bool Move(Vector2Int moveDir)
    {
        var target = Position + moveDir;
        LookAt(target);
        return MoveToPosition(target);
    }

    public Vector2Int DirectionTo(Vector2Int target)
    {
        var direction = target - Position;
        var absDir = new Vector2Int(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        direction.Clamp(new Vector2Int(-1, -1), new Vector2Int(1, 1));
        return absDir.x >= absDir.y 
            ? new Vector2Int(direction.x, 0) 
            : new Vector2Int(0, direction.y);
    }

    public bool MoveTowards(Vector2Int newPos)
    {
        var direction = DirectionTo(newPos);
        return Move(direction);
    }

    public bool MoveToPosition(Vector2Int newPos)
    {
        if (!grid.InBounds(newPos.x, newPos.y))
            return false;

        if (!grid.IsWalkable(newPos.x, newPos.y))
            return false;

        grid.Nodes[Position.y, Position.x].unit = null;
        grid.Nodes[newPos.y, newPos.x].unit = this;
        
        prevPosition = Position;
        position = newPos;
        state = GridUnitState.Moving;
        return true;
    }

    public void MoveToPosition(Vector3 newWorldPos)
    {
        var newPos = grid.WorldToCell(newWorldPos);
        MoveToPosition(newPos);
    }

    public void Respawn()
    {
        position = initialPosition;
        transform.position = grid.CellToWorld(position).SetY(1);;
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
        grid = FindObjectOfType<GridLayer>();
        position = grid.WorldToCell(transform.position);
        prevPosition = position;
        transform.position = grid.CellToWorld(position).SetY(1);
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
