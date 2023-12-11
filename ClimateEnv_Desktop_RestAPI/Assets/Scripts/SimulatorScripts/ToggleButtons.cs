using UnityEngine;

public class ToggleButtons : MonoBehaviour
{
    public Color initialColor = Color.white; // Initial color of the buttons
    public GameObject[] buttonBParts; // Array of objects for Button B
    public GameObject otherButton; // Reference to the other button

    private Color originalColorA; // Store the original color of button A
    private Color[] originalColorsB; // Store the original colors of button B parts

    private void Start()
    {
        // Store the original colors of both buttons
        originalColorA = GetComponent<Renderer>().material.color;

        // Store the original colors of Button B parts
        originalColorsB = new Color[buttonBParts.Length];
        for (int i = 0; i < buttonBParts.Length; i++)
        {
            originalColorsB[i] = buttonBParts[i].GetComponent<Renderer>().material.color;
        }
    }

    private void OnMouseDown()
    {
        // Check which button was clicked and toggle colors accordingly
        if (gameObject.CompareTag("ButtonA"))
        {
            // Button A clicked, change its color to red and reset Button B's color
            GetComponent<Renderer>().material.color = Color.red;
            foreach (GameObject part in buttonBParts)
            {
                part.GetComponent<Renderer>().material.color = originalColorA;
            }
        }
        else if (gameObject.CompareTag("ButtonB"))
        {
            // Button B clicked, change its color to red and reset Button A's color
            GetComponent<Renderer>().material.color = Color.red;
            otherButton.GetComponent<Renderer>().material.color = originalColorA;
            foreach (GameObject part in buttonBParts)
            {
                part.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
