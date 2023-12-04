using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script extracts the points from the Bezier segments and sets them as positions for the line renderer.
// It is assigned to every BezierPoints object at the interactive Table in the editor
// (Table - Paths - [specific panel/Content Card name] - BezierPoints).

public class BezierVisualizer : MonoBehaviour
{
    // an array of Bezier segments, assigned in the editor
    public BezierCurve[] BezierSegments;

    // a renderer of the line, assigned in the editor
    public LineRenderer Renderer;

    void Start()
    {
        // create a new list of points with 3-dimensional coordinates
        List<Vector3> pointList = new List<Vector3>();

        // save all points of each Bezier segment in this new pointList (concatenation):
        for (int i = 0; i < this.BezierSegments.Length; i++)
        {
            // save points of the segment in a list
            List<Vector3> points = this.BezierSegments[i].GetPoints();

            // append each of the points to pointList
            foreach (Vector3 point in points)
            {
                pointList.Add(point);
            }
        }

        // set amount of positions in line renderer to amount of all the points in pointList
        this.Renderer.positionCount = pointList.Count;
        /*
        // create new array of 3-dimensional positions with same length as pointList
        Vector3[] positions = new Vector3[pointList.Count];

        // fill array with points from pointList (copy pointList into array positions)
        for (int i = 0; i < pointList.Count; i++)
        {
            positions[i] = pointList[i];
        }

        // set positions as positions for the line renderer
        this.Renderer.SetPositions(positions);
        */

        this.Renderer.SetPositions(pointList.ToArray());
    }
}