using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the rotation of the loading circle while the simulator website is loaded in the simulator panel.
// It is assigned to the Progress object of the Loading Circle in the Simulator panel.

public class LoadingCircle : MonoBehaviour
{
    // defines the Transform of a rectangle and a speed of rotation for the circle
    private RectTransform rectComponent;
    private float rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        // initialize rectangle Transform
        rectComponent = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the component with the defined speed and the time from the last to the current frame
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}