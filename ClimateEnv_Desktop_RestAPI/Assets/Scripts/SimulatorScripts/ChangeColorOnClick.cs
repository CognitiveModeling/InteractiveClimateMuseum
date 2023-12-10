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
                // Function 1 action
                Set2022Script();
                break;
            case 2:
                // Function 2 action
                Debug.Log("Function 2 triggered");
                break;
            case 3:
                // Function 3 action
                Debug.Log("Function 3 triggered");
                break;
            case 4:
                // Function 4 action
                Debug.Log("Function 4 triggered");
                break;
            case 5:
                // Function 5 action
                Debug.Log("Function 5 triggered");
                break;
            default:
                break;
        }
    }

    private void Set2022Script()
{
    // Find the Set2022 script component attached to a GameObject
    Set2022 set2022Script = FindObjectOfType<Set2022>();

    if (set2022Script != null)
    {
        // Access the method from Set2022 script to perform actions
        set2022Script.set2022();
    }
}
}
