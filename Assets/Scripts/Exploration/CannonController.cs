using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer line;
    private int numbPoint = 50;
    private Vector3[] positions;
    public Transform point0, point1, point2;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = numbPoint;
        positions = new Vector3[numbPoint];
        DrawQuadraticCurve();
    }
    private void Update()
    {
        DrawQuadraticCurve();
    }        


    private void DrawLinearCurve()
    {
        for(int i = 1; i < numbPoint + 1; i++)
        {
            float t = i / numbPoint;
            positions[i - 1] = CalculateLinearBezierPoint(t, point0.position, point1.position);
        }
        line.SetPositions(positions);
    }
    private void DrawQuadraticCurve()
    {
        Vector2[] actualPositions =
        {
            new Vector2(point0.position.y, point0.position.z),
            new Vector2(point1.position.y, point1.position.z),
            new Vector2(point2.position.y, point2.position.z),
        };
        for (int i = 1; i < numbPoint + 1; i++)
        {
            float t = i / numbPoint;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, actualPositions[0], actualPositions[1], actualPositions[2]);
        }
        foreach (Vector3 pos in positions)
        {
          //  line.SetPosition(new Vector3(pos.x);
        }
    }

    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private Vector2 CalculateQuadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector2 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
