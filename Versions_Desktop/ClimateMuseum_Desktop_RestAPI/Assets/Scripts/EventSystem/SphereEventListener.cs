using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEventListener : MonoBehaviour
{
    public GameObject Planet;

    private Material material;

    private bool pulsing = false;
    private bool rotating = false;
    private bool coloring = false;

    private float passedTime = 0f;

    private Vector3 InitialColor;
    private Vector3 TargetColor;

    void Start()
    {
        EventSystemBase.aCollisionEvent += this.processCollisionEvent;
        // the blue material is on position three in the materials array (as you can see in the editor as well)
        this.material = this.Planet.GetComponentInChildren<MeshRenderer>().materials[3];
        Color color = material.color;
        this.InitialColor = new Vector3(color.r, color.g, color.b);
        this.TargetColor = new Vector3(0.3f, .1f, .1f);
    }

    void Update()
    {
        if (this.pulsing)
        {
            this.pulse();
        }
        if (this.rotating)
        {
            this.Planet.transform.Rotate(Vector3.up, Time.deltaTime * 10.0f);
        }
        if (this.coloring)
        {
            this.colorPulse();
        }
    }

    void processCollisionEvent(string type)
    {
        if (type == "pulse")
        {
            this.pulsing = !this.pulsing;
            this.passedTime = 0.0f;
        }
        else if (type == "rotation")
        {
            this.rotating = !this.rotating;
        }
        else if (type == "color")
        {
            this.coloring = !this.coloring;
        }
    }

    private void pulse()
    {
        this.passedTime += Time.deltaTime;
        Planet.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(2.0f, 2.0f, 2.0f),
            Mathf.PingPong(this.passedTime, 1));
    }

    private void colorPulse()
    {
        this.passedTime += Time.deltaTime;
        Vector3 colorVector = Vector3.Lerp(this.InitialColor, this.TargetColor,
            Mathf.PingPong(this.passedTime, 1));
        this.material.color = new Color(colorVector.x, colorVector.y, colorVector.z);
    }
}
