using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public class EnemyController : UnitController
{
    private Vector2Int nextPosition;
    private float timer;
    private float waitTime;

    [Range(1f, 50f)] public float speed = 20;

    [Range(0.01f, 1f)] public float refreshInterval = 0.2f;

    GridNode[] path;
    int targetIndex;

    void Start()
    {
        unit = GetComponent<GridUnit>();
        waitTime = Random.Range(1f, 2f);
        CalculateNextPosition();

        StartCoroutine(RequestPath());
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
        nextPosition = unit.position + new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
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
        if (unit == null || unit.grid == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(unit.grid.CellToWorld(nextPosition), Vector3.one);
        
        if (path == null)
            return;

        for (int i = targetIndex; i < path.Length; i++)
        {
            Vector2 lineStart = (i == targetIndex) ? (transform.position) : path[i - 1].transform.position;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(path[i].transform.position, 0.2f);
            Gizmos.DrawLine(lineStart, path[i].transform.position);
        }
    }

    IEnumerator RequestPath()
    {
        Vector3 targetPositionOld = target.position + Vector3.forward;

        while (true)
        {
            var targetPos = target.position;
            if (targetPositionOld != targetPos)
            {
                targetPositionOld = targetPos;
                path = GridPathfinding.RequestPath(transform.position, targetPos);
                StopCoroutine(nameof(FollowPath));
                StartCoroutine(nameof(FollowPath));
            }

            yield return new WaitForSeconds(refreshInterval);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length <= 0)
            yield break;

        targetIndex = 0;
        Vector3 currentWaypoint = path[0].transform.position;

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                    yield break;
                currentWaypoint = path[targetIndex].transform.position;
            }

            yield return null;
        }
    }
}