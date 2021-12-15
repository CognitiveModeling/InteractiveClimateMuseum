using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class HandleLoadingScreen : MonoBehaviour
{
  public GameObject LoadingScreen;

  public Browser BrowserInstance;

  private bool queryRunning = false;

  // Update is called once per frame
  void Update()
  {
    if (this.LoadingScreen.activeSelf && !this.queryRunning)
    {
      this.queryRunning = true;
      StartCoroutine(this.checkBrowserReady());
    }
  }
  private IEnumerator checkBrowserReady()
  {
    IPromise<JSONNode> promise = null;
    // the loading screen overlaps with the background, making the text hard to read
    try
    {
      promise = this.BrowserInstance.EvalJS("document.getElementsByClassName(\"primary-temp-value\")[0].innerHTML");
    }
    catch (JSException jse)
    {
      Debug.Log(jse.Message);
    }
    // not a nice solution, but it works: until the element is not defined (i.e. we get an error), we keep the loading screen,
    // if we get a value, we deactivate the loading screen
    if (promise != null)
    {
      yield return promise.ToWaitFor();
      try
      {
        JSONNode node = promise.Value;
        this.LoadingScreen.SetActive(false);
      }
      catch (JSException jse)
      {
        Debug.Log(jse.Message);
      }
    }

    this.queryRunning = false;
    yield return null;
  }
}
