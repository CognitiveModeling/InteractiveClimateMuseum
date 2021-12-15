using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrelationOnEnable : MonoBehaviour
{
    private bool initialized = false;
    private float currentToggleOffset = 0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!initialized)
        {
            initialized = true;
            StartCoroutine(this.initializationCoroutine());

        }
    }

    private IEnumerator initializationCoroutine()
    {
        yield return 1; // Wait for 1 frame
        
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Calculate and apply toggle offset of correlation tabs
            
            if (i > 0)
            {
                RectTransform rectangleForPosition;
                currentToggleOffset += this.transform.GetChild(i).transform.GetComponent<RectTransform>().anchoredPosition.x - 
                    this.transform.GetChild(i-1).GetComponent<RectTransform>().anchoredPosition.x;
                rectangleForPosition = this.transform.GetChild(i).transform.Find("Content").GetComponent<RectTransform>();
                rectangleForPosition.anchoredPosition = new Vector2(rectangleForPosition.anchoredPosition.x - currentToggleOffset, 
                    rectangleForPosition.anchoredPosition.y);
            }

        }

        this.gameObject.SetActive(false);

        yield return null;
    }
}
