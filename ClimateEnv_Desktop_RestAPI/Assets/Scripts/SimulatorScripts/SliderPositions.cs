using UnityEngine;
using System.Collections;

public class SliderPositions : MonoBehaviour
{
    public int functionTrigger = 1; // Variable to indicate the triggered function (1 to 5)
    public float minX = -0.24f; // Define the minimum X position
    public float maxX = 0.24f; // Define the maximum X position
    public float[] snapPoints; // Array to hold the predefined snap points
    private Color originalColor; // Store the original color of the object
    private bool isPressed = false; // Indicates if the mouse button is pressed
    private Vector3 initialMousePos; // Store the initial mouse position when clicked
    private float initialLocalObjX; // Store the initial local X position of the object
    public float movementSpeed = 0.0014f; // Adjust this value to change the movement speed
    public EnvironmentUpdate environmentUpdate; // Reference to the EnvironmentUpdate script
    private int currentIndex = 0; // Current index for snapPoints array
    private bool loopActive = false; // Flag to control loop activation

    private void Start()
    {
        // Store the original color and local X position of the object
        originalColor = GetComponent<Renderer>().material.color;
        initialLocalObjX = transform.localPosition.x;

        // Calculate snap points based on the specified range
        CalculateSnapPoints();
    }

    private void Update()
    {
        if (loopActive)
        {
            LoopThroughStates();
        }
    }

    private void CalculateSnapPoints()
    {
        // Ensure the snap points array is initialized with five slots
        snapPoints = new float[5];

        // Calculate the step size between snap points
        float stepSize = (maxX - minX) / 4;

        // Calculate and store the five predefined snap points within the range
        for (int i = 0; i < 5; i++)
        {
            snapPoints[i] = minX + stepSize * i;
        }
    }

    public void LoopThroughStates(bool onOff)
    {
        loopActive = onOff;
    }

    private void LoopThroughStates()
    {
        currentIndex = (currentIndex + 1) % snapPoints.Length;
        float targetSnapPoint = snapPoints[currentIndex];

        // Move to the next snap point
        StartCoroutine(MoveToSnapPoint(targetSnapPoint, 0.5f));
    }

    private IEnumerator MoveToSnapPoint(float targetX, float duration)
    {
        float initialX = transform.localPosition.x;
        float timer = 0f;

        while (timer < duration)
        {
            float newX = Mathf.Lerp(initialX, targetX, timer / duration);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnMouseDown()
    {
        // Change the color to yellow only when the mouse button is pressed down
        GetComponent<Renderer>().material.color = Color.yellow;
        isPressed = true;

        // Store the initial mouse position and initial local X position of the object
        initialMousePos = Input.mousePosition;
        initialMousePos.z = Vector3.Distance(Camera.main.transform.position, transform.position);
        initialLocalObjX = transform.localPosition.x;
    }

    private void OnMouseDrag()
    {
        if (isPressed)
        {
            // Calculate the change in mouse X position since the click
            float deltaX = (Input.mousePosition.x - initialMousePos.x) * movementSpeed;

            // Calculate new object local X position based on the initial position and mouse movement
            float newLocalX = initialLocalObjX + deltaX;

            // Clamp the new X position within the specified range
            newLocalX = Mathf.Clamp(newLocalX, minX, maxX);

            // Update only the object's local X position while dragging
            transform.localPosition = new Vector3(newLocalX, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnMouseUp()
    {
        // Revert the color back to the original color when the mouse button is released
        GetComponent<Renderer>().material.color = originalColor;
        isPressed = false;

        // Find the closest predefined snap point to the current object's X position
        float closestSnapPoint = snapPoints[0];
        float shortestDistance = Mathf.Abs(transform.localPosition.x - snapPoints[0]);
        foreach (float snapPoint in snapPoints)
        {
            float distance = Mathf.Abs(transform.localPosition.x - snapPoint);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestSnapPoint = snapPoint;
            }
        }

        // Snap the object to the closest predefined snap point
        transform.localPosition = new Vector3(closestSnapPoint, transform.localPosition.y, transform.localPosition.z);

        // Determine the triggered function based on the closest snap point
        for (int i = 0; i < snapPoints.Length; i++)
        {
            if (snapPoints[i] == closestSnapPoint)
            {
                functionTrigger = i + 1;
                break;
            }
        }

        // Perform action based on the triggered function (functionTrigger)
        PerformFunction();
    }

    private void PerformFunction()
    {
        // Perform actions based on the triggered function (functionTrigger)
        switch (functionTrigger)
        {
            case 1:
                // Set yearSelector to 0 for function 1
                SetEnvironmentUpdateYearSelector(0);
                break;
            case 2:
                // Set yearSelector to 1 for function 2
                SetEnvironmentUpdateYearSelector(1);
                break;
            case 3:
                // Set yearSelector to 2 for function 3
                SetEnvironmentUpdateYearSelector(2);
                break;
            case 4:
                // Set yearSelector to 3 for function 4
                SetEnvironmentUpdateYearSelector(3);
                break;
            case 5:
                // Set yearSelector to 4 for function 5
                SetEnvironmentUpdateYearSelector(4);
                break;
            default:
                break;
        }
    }

    private void SetEnvironmentUpdateYearSelector(int value)
    {
        if (environmentUpdate != null)
        {
            // Set the yearSelector variable in EnvironmentUpdate script
            environmentUpdate.yearSelector = value;
        }
    }
}
