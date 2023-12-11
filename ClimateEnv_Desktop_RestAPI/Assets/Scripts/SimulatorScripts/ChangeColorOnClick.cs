using UnityEngine;

public class ChangeColorOnClick : MonoBehaviour
{
    public int functionTrigger = 1; // Variable to indicate the triggered function (1 to 5)
    public float minX = -0.24f; // Define the minimum X position
    public float maxX = 0.24f; // Define the maximum X position
    public float[] snapPoints; // Array to hold the predefined snap points
    private Color originalColor;
    private bool isPressed = false;
    private Vector3 initialMousePos;
    private float initialLocalObjX;
    public float movementSpeed = 0.0014f; // Adjust this value to change the movement speed
    Set2022 set2022Script = FindObjectOfType<Set2022>();

    private void Start()
    {
        // Store the original color and local X position of the object
        originalColor = GetComponent<Renderer>().material.color;
        initialLocalObjX = transform.localPosition.x;

        // Calculate snap points based on the specified range
        CalculateSnapPoints();
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
    // Find the EnvironmentUpdate script component attached to a GameObject
    EnvironmentUpdate environmentUpdate = FindObjectOfType<EnvironmentUpdate>();

    if (environmentUpdate != null)
    {
        // Set the yearSelector variable in EnvironmentUpdate script
        environmentUpdate.yearSelector = value;
    }
}

}
