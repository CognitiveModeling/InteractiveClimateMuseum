using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualizer : MonoBehaviour
{
    public Transform[] ControlPoints;

    public LineRenderer Renderer;

    public bool DrawSpheres = true;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> pointList = new List<Vector3>();

        for (int i = 0; i < this.ControlPoints.Length - 1; i++)
        {
            Vector3 start = this.ControlPoints[i].position;
            Vector3 end = this.ControlPoints[i + 1].position;

            float dist = Vector2.Distance(new Vector2(start.x, start.z), new Vector2(end.x, end.z));
            float angularOffset = Mathf.Atan2((end.z - start.z), (end.x - start.x));
            float radius = dist * Mathf.Sin(Mathf.PI * .25f);

            float mx = radius * Mathf.Cos(Mathf.PI * .25f + angularOffset);
            float mz = radius * Mathf.Sin(Mathf.PI * .25f + angularOffset);
            float my = (end.y + start.y) * .5f;

            Vector3 middle = start + new Vector3(mx, my, mz);

            if(this.DrawSpheres)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = middle;
                sphere.name = "Sphere" + i;
            }

            for (float t = 0.0f; t < 1.0f; t += .05f)
            {
                pointList.Add(LineVisualizer.GetBezierPoint(start, middle, end, t));
            }
            pointList.Add(end);
        }

        this.Renderer.positionCount = pointList.Count;

        Vector3[] positions = new Vector3[pointList.Count];

        for (int i = 0; i < pointList.Count; i++)
        {
            positions[i] = pointList[i];
        }

        this.Renderer.SetPositions(positions);

        /*
        this.Renderer.positionCount = this.ControlPoints.Length;

        Vector3[] positions = new Vector3[this.ControlPoints.Length];

        for (int i = 0; i < this.ControlPoints.Length; i++)
        {
            positions[i] = this.ControlPoints[i].position;
        }

        this.Renderer.SetPositions(positions);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetBezierPoint(Vector3 start, Vector3 support, Vector3 end, float t)
    {
        return Vector3.Lerp(Vector3.Lerp(start, support, t), Vector3.Lerp(support, end, t), t);
    }
}
