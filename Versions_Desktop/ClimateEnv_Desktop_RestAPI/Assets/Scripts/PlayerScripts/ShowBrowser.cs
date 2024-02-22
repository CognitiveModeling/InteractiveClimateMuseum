using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowBrowser : MonoBehaviour
{

    public GameObject browser;

    // Start is called before the first frame update
    void Start()
    {
        browser.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            Renderer browserRenderer = browser.GetComponent<Renderer>();
            Collider browserCollider = browser.GetComponent<Collider>();

            if (!browserRenderer.enabled)
            {
                // Make browser visible and interactable
                browserRenderer.enabled = true;
                browserCollider.enabled = true;
            }
            else
            {
                // Make browser invisible and not interactable
                browserRenderer.enabled = false;
                browserCollider.enabled = false;
            }
        }
  
    }
}
