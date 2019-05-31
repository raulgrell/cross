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
        DrawLinearCurve();
    }
    private void Update()
    {
       // DrawQuadraticCurve();
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
        for (int i = 1; i < numbPoint + 1; i++)
        {
            float t = i / numbPoint;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        }
        line.SetPositions(positions);
    }

    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }


    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
