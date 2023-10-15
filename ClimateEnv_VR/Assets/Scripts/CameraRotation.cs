using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 2.0f; // Adjust this to control the rotation speed
    public Transform player; // Assign your player or target object here

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    void Update()
    {
        // Capture mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate rotation based on mouse input
        yaw += mouseX * rotationSpeed;
        pitch -= mouseY * rotationSpeed;

        // Limit the pitch to prevent flipping the camera
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Apply the rotation to the camera and player (if desired)
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (player != null)
        {
            // Rotate the player (or target) separately if you want to
            player.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        }
    }
}
