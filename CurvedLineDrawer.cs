using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedLineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] controlPoints;

    private List<Vector3> points = new List<Vector3>();
    public int curveSegmentCount = 50;

    void Start()
    {
        lineRenderer.positionCount = curveSegmentCount * (controlPoints.Length - 1);
        DrawCurvedLine();
    }

    void DrawCurvedLine()
    {
        for (int i = 0; i < controlPoints.Length - 1; i++)
        {
            for (int j = 0; j < curveSegmentCount; j++)
            {
                float t = (float)j / (curveSegmentCount - 1);
                Vector3 point = GetCatmullRomPoint(t, controlPoints[i % controlPoints.Length].position, controlPoints[(i + 1) % controlPoints.Length].position, controlPoints[(i + 2) % controlPoints.Length].position, controlPoints[(i + 3) % controlPoints.Length].position);
                points.Add(point);
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }

    Vector3 GetCatmullRomPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float tt = t * t;
        float ttt = tt * t;
        float q1 = -ttt + 2.0f * tt - t;
        float q2 = 3.0f * ttt - 5.0f * tt + 2.0f;
        float q3 = -3.0f * ttt + 4.0f * tt + t;
        float q4 = ttt - tt;
        float factor = 0.5f;
        Vector3 point = 0.5f * (p0 * q1 + p1 * q2 + p2 * q3 + p3 * q4);

        return point;
    }
}
