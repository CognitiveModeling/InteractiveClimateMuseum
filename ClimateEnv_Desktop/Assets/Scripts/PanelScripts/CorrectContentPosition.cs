using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script sets the content of each child/sub tab (tabs inside "General Info" or "Correlation")
// to the correct (readable) position in the panels.
// It itself is not assigned to any objects in the editor.

public class CorrectContentPosition : MonoBehaviour
{
    // indicates if positioning has already happened
    private bool initialized = false;

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

        // for each child tab, set content to correct position
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // every content has same global position (position of content of first element of general information)
            this.transform.GetChild(i).Find("Content").position = this.transform.parent.parent.GetChild(0).Find("Tab Container").GetChild(0).Find("Content").position;
        }

        yield return null;
    }
}

