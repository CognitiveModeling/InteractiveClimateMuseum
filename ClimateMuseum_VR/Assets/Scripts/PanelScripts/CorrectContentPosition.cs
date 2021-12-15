using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectContentPosition : MonoBehaviour
{
    private bool initialized = false;

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
            // Every content has same global position (position of content of first element of general information)
            this.transform.GetChild(i).Find("Content").position = this.transform.parent.parent.GetChild(0).Find("Tab Container").GetChild(0).Find("Content").position; 
        }

        yield return null;
    }
}

