using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// the idea is to have a controllable path segment, that can be concatenated by the BezierVisualizer;
// this implementation is close to the tutorial version here:
// https://www.habrador.com/tutorials/interpolation/2-bezier-curve/
public class BezierCurve : MonoBehaviour
{
    //Has to be at least 4 so-called control points
    public Transform Start;
    public Transform ControlPointStart;
    public Transform ControlPointEnd;
    public Transform End;

    //Easier to use ABCD for the positions of the points so they are the same as in the tutorial image
    private Vector3 A;
    private Vector3 B;
    private Vector3 C;
    private Vector3 D;

    //Display without having to press play
    void OnDrawGizmos()
    {
        this.A = this.Start.position;
        this.B = this.ControlPointStart.position;
        this.C = this.ControlPointEnd.position;
        this.D = this.End.position;

        //The Bezier curve's color
        Gizmos.color = Color.white;

        //The start position of the line
        Vector3 lastPos = this.A;

        //The resolution of the line
        //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
        float resolution = 0.02f;

        //How many loops?
        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            //Which t position are we at?
            float step = i * resolution;

            //Find the coordinates between the control points with a Catmull-Rom spline
            Vector3 newPos = this.DeCasteljausAlgorithm(step);

            //Draw this line segment
            Gizmos.DrawLine(lastPos, newPos);

            //Save this pos so we can draw the next line segment
            lastPos = newPos;
        }
    }

    public List<Vector3> GetPoints()
    {
        List<Vector3> points = new List<Vector3>();

        this.A = this.Start.position;
        this.B = this.ControlPointStart.position;
        this.C = this.ControlPointEnd.position;
        this.D = this.End.position;
        
        Vector3 lastPos = this.A;
        
        float resolution = 0.02f;
        
        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            points.Add(lastPos);

            float step = i * resolution;
            
            Vector3 newPos = this.DeCasteljausAlgorithm(step);
            
            lastPos = newPos;
        }

        points.Add(lastPos);

        return points;
    }

    //The De Casteljau's Algorithm
    private Vector3 DeCasteljausAlgorithm(float step)
    {
        //Linear interpolation = lerp = (1 - t) * A + t * B
        //Could use Vector3.Lerp(A, B, t)

        //To make it faster
        float oneMinusT = 1f - step;

        //Layer 1
        Vector3 Q = oneMinusT * this.A + step * this.B;
        Vector3 R = oneMinusT * this.B + step * this.C;
        Vector3 S = oneMinusT * this.C + step * this.D;

        //Layer 2
        Vector3 P = oneMinusT * Q + step * R;
        Vector3 T = oneMinusT * R + step * S;

        //Final interpolated position
        Vector3 U = oneMinusT * P + step * T;

        return U;
    }
}
