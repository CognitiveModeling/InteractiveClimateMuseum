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
    private bool isMoving = false; // Flag to check if object is in motion
    private float delayDuration = 2.0f; // Duration to stay at each snap point
    private Coroutine loopCoroutine; // Coroutine for looping through snap points


    private void Start()
    {
        // Store the original color and local X position of the object
        originalColor = GetComponent<Renderer>().material.color;
        initialLocalObjX = transform.localPosition.x;

        // Calculate snap points based on the specified range
        CalculateSnapPoints();

        // Start looping through states every 2 seconds
        //loopActive = false;
        //loopCoroutine = StartCoroutine(LoopThroughStates());
        
    }

    private void Update()
    {
        if (loopActive && !isMoving)
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

     // Public function to toggle loop on/off
    public void LoopThroughStates(bool onOff)
    {
        loopActive = onOff;

        if (loopActive)
        {
            // Start or resume the coroutine based on the loopActive flag
            if (loopCoroutine == null)
            {
                loopCoroutine = StartCoroutine(LoopThroughStatesCoroutine());
            }
            else
            {
                // If coroutine already running, resume it
                StartCoroutine(LoopThroughStatesCoroutine());
            }
        }
        else
        {
            // If loop is turned off, stop the coroutine
            if (loopCoroutine != null)
            {
                StopCoroutine(loopCoroutine);
                loopCoroutine = null;
            }
        }
    }

    private IEnumerator LoopThroughStatesCoroutine()
    {
        int targetIndex = 0; // Initialize the target index

        while (loopActive)
        {
            float targetSnapPoint = snapPoints[targetIndex]; // Set the target snap point

            // Move to the next snap point if the object is not at the current snap point
            while (!IsApproximately(transform.localPosition.x, targetSnapPoint))
            {
                // Move towards the next snap point
                yield return StartCoroutine(MoveToSnapPoint(targetSnapPoint, 0.5f)); // Adjust the movement duration here
            }

            currentIndex = targetIndex; // Update currentIndex to the current target index
            functionTrigger = currentIndex+1; // Update functionTrigger based on the current index

            // Perform actions based on the current snap point
            PerformFunction();

            // Calculate the next target index for looping
            targetIndex = (targetIndex + 1) % snapPoints.Length;

            // Wait at the snap point for the specified delay duration
            yield return new WaitForSeconds(delayDuration);
        }
    }

    // Helper method to get the index of the nearest snap point based on the current position
    private int GetNearestSnapPointIndex(float currentX)
    {
        int nearestIndex = 0;
        float shortestDistance = Mathf.Abs(currentX - snapPoints[0]);

        for (int i = 1; i < snapPoints.Length; i++)
        {
            float distance = Mathf.Abs(currentX - snapPoints[i]);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }


    private IEnumerator LoopThroughStates()
    {
        while (true)
        {
            currentIndex = (currentIndex + 1) % snapPoints.Length;
            float targetSnapPoint = snapPoints[currentIndex];

            // Move to the next snap point
            yield return StartCoroutine(MoveToSnapPoint(targetSnapPoint, 0.5f)); // Adjust the movement duration here
            
            functionTrigger = currentIndex+1;
            // Perform actions based on the current snap point
            PerformFunction();

            // Wait at the snap point for the specified delay duration
            yield return new WaitForSeconds(delayDuration);
            
        }
    }

    // Helper method to check if two float values are approximately equal
    private bool IsApproximately(float a, float b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a - b) < tolerance;
    }

    private IEnumerator MoveToSnapPoint(float targetX, float duration)
    {
        isMoving = true; // Set the flag to indicate object movement
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
        isMoving = false; // Reset the flag after movement is complete
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
