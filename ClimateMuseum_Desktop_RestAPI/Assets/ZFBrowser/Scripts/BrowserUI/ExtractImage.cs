using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class ExtractImage : MonoBehaviour
{
    public Browser browser;

    public Browser refbrowser;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            StartCoroutine(this.fetchTemperaturePrediction());
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine(this.fetchGraph(1));
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log(GUIUtility.systemCopyBuffer);
        }
    }

    private IEnumerator fetchTemperaturePrediction()
    {
        var promise = this.browser.EvalJS("document.getElementsByClassName(\"primary-temp-value\")[0].innerHTML");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
    }

    private IEnumerator fetchGraph(int index)
    {
        var promise = this.browser.EvalJS("document.getElementsByClassName(\"chartjs-render-monitor\")[" + index + "].toDataURL(\"img/png\")");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
        this.refbrowser.EvalJS("document.getElementById(\"image-container\").src = '" + promise.Value + "';");
        this.refbrowser.EvalJS("document.getElementById(\"image-container\").width = '" + 500 + "';");
        this.refbrowser.EvalJS("document.getElementById(\"image-container\").height = '" + 500 + "';");

        // console.log(document.getElementsByClassName('chartjs-render-monitor').item(1).getContext('2d').data('chart'))
        promise = this.browser.EvalJS("console.log(document.getElementsByClassName(\"chartjs-render-monitor\").item(0).innerHTML)");
        yield return promise.ToWaitFor();
        Debug.Log("promised value: " + promise.Value);
    }
}
