using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script sets each child/sub tab's toggle to the correct (clickable) position.
// It itself is not assigned in the editor.

public class CorrelationOnEnable : MonoBehaviour
{
    // indicates if positioning has already happened
    private bool initialized = false;

    // difference of a tab's toggle to its neighbouring toggle
    private float currentToggleOffset = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        // if initialization has not happened yet
        if (!initialized)
        {
            // set initialization boolean to true
            initialized = true;
            // start the initialization coroutine
            StartCoroutine(this.initializationCoroutine());
        }
    }

    private IEnumerator initializationCoroutine()
    {
        // wait for 1 frame
        yield return 1;
        
        // for each child tab:
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // if it is not the first child tab: calculate and apply toggle offset of correlation tabs
            if (i > 0)
            {
                // define a rectangle
                RectTransform rectangleForPosition;
                // calculate toggle offset for the current child tab dependent on the one before (left of it)
                currentToggleOffset += this.transform.GetChild(i).transform.GetComponent<RectTransform>().anchoredPosition.x - 
                    this.transform.GetChild(i-1).GetComponent<RectTransform>().anchoredPosition.x;
                // initialize the rectangle with position values from Content
                rectangleForPosition = this.transform.GetChild(i).transform.Find("Content").GetComponent<RectTransform>();
                // set anchored position of rectangle (x-coordinates with the calculated toggle offset)
                rectangleForPosition.anchoredPosition = new Vector2(rectangleForPosition.anchoredPosition.x - currentToggleOffset, 
                    rectangleForPosition.anchoredPosition.y);
            }
        }

        // deactivate general tab "Correlations" (activation happens in Start() of script PanelRenderer)
        this.gameObject.SetActive(false);

        yield return null;
    }
}
