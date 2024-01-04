using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierVisualizer : MonoBehaviour
{
    public BezierCurve[] BezierSegments;

    public LineRenderer Renderer;

    void Start()
    {
        List<Vector3> pointList = new List<Vector3>();

        for (int i = 0; i < this.BezierSegments.Length; i++)
        {
            List<Vector3> points = this.BezierSegments[i].GetPoints();

            foreach (Vector3 point in points)
            {
                pointList.Add(point);
            }
        }

        this.Renderer.positionCount = pointList.Count;

        Vector3[] positions = new Vector3[pointList.Count];

        for (int i = 0; i < pointList.Count; i++)
        {
            positions[i] = pointList[i];
        }

        this.Renderer.SetPositions(positions);
    }
}
