using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using UnityEngine.EventSystems;

public class VRButtons : MonoBehaviour
{
    
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean interactAction;
    public Color clickedColor;

    private Button button;
    private Color originalColor;

    private void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.colors.normalColor;
    }

    private void Update()
    {
        Debug.Log("interactAction.GetStateDown(handType): " + interactAction.GetStateDown(handType));
        
        if (interactAction.GetStateDown(handType))
        {
            // Check if the controller is colliding with the button.
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f); // Adjust the radius as needed.

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == button.gameObject)
                {
                    // Change the button color when clicked.
                    ChangeButtonColorOnClick();
                    break;
                }
            }
        }
    }

    private void ChangeButtonColorOnClick()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = clickedColor;
        button.colors = colors;
    }

    public void ResetButtonColor()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = originalColor;
        button.colors = colors;
    }
}