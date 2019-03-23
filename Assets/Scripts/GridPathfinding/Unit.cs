using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;

    [Range(1f, 50f)]
    public float speed = 20;

    [Range(0.01f, 1f)]
    public float refreshInterval;

    Vector2[] path;
    int targetIndex;

    IEnumerator Start()
    {
        Vector2 targetPositionOld = (Vector2)(target.position) + Vector2.up;

        while (true)
        {
            if (targetPositionOld != (Vector2)(target.position))
            {
                targetPositionOld = target.position;
                path = Pathfinding.RequestPath(transform.position, target.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }

            yield return new WaitForSeconds(refreshInterval);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length <= 0)
            yield break;
        
        targetIndex = 0;
        Vector2 currentWaypoint = path[0];

        while (true)
        {
            if ((Vector2)(transform.position) == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                    yield break;
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path == null)
            return;

        for (int i = targetIndex; i < path.Length; i++)
        {
            Vector2 lineStart = (i == targetIndex) ? (Vector2)(transform.position) : path[i - 1];

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(path[i], 0.2f);
            Gizmos.DrawLine(lineStart, path[i]);
        }
    }
}
