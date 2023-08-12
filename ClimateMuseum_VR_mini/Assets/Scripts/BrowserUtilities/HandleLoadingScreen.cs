using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

// This script handles the presentation of a loading screen while the browser is loaded.
// The script is assigned to the general simulator's tab panel and its start screen.

public class HandleLoadingScreen : MonoBehaviour
{
    // a loading screen, a browser instance and a boolean indicating the query's state
    public GameObject LoadingScreen;

    public Browser BrowserInstance;

    private bool queryRunning = false;

    // Update is called once per frame
    void Update()
    {
        // if the loading screen is active and the query does not run
        if (this.LoadingScreen.activeSelf && !this.queryRunning)
        {
            // query runs now
            this.queryRunning = true;
            // start the coroutine that checks if the browser is ready
            StartCoroutine(this.checkBrowserReady());
        }
    }

    // coroutine that checks if the browser is ready
    private IEnumerator checkBrowserReady()
    {
        // initialize a node
        IPromise<JSONNode> promise = null;

        // the loading screen overlaps with the background, making the text hard to read
        // try to fill the node with an element on the website (here: the temperature increase)
        try
        {
            promise = this.BrowserInstance.EvalJS("document.getElementsByClassName(\"primary-temp-value\")[0].innerHTML");
        }
        catch (JSException jse)
        {
            Debug.Log(jse.Message);
        }
        
        // not a nice solution, but it works: until the element is not defined (i.e. we get an error), we keep the loading screen,
        // if we get a value in the variable promise, we deactivate the loading screen
        if (promise != null)
        {
            yield return promise.ToWaitFor();
            try
            {
                JSONNode node = promise.Value;
                // deactivate the loading screen
                this.LoadingScreen.SetActive(false);
            }
            catch (JSException jse)
            {
                Debug.Log(jse.Message);
            }
        }

        // query is set to false again
        this.queryRunning = false;
        yield return null;
    }
}
