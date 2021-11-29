using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateCorrelationFrames : MonoBehaviour
{    
    public delegate void OnFrameStateChange(string tableCardName);

    public static event OnFrameStateChange OnFrameStateChangeEvent;
    
    public static CorrelationDictionary correlations = new CorrelationDictionary();

    public void OnMouseDown()
    {
        OnFrameStateChangeEvent(this.gameObject.name);
    }

    private void ResponseToFrameStateChange(string correlationTableCardName)
    {
        string[] correlationsArray = null;
        if(ActivateCorrelationFrames.correlations.correlationDictionary.TryGetValue(this.gameObject.name, out correlationsArray))
        {
            foreach(string correlation in correlationsArray)
            {
                if (correlation == correlationTableCardName)
                {
                    GameObject correlationFrame = this.transform.Find("Frame Correlate").gameObject;

                    if (correlationFrame.activeSelf == false)
                    {
                        correlationFrame.SetActive(true);
                    }
                    
                    else
                    {
                        correlationFrame.SetActive(false);
                    } 
                }
            }
        }
    }

    void Start()
    {
        ActivateCorrelationFrames.OnFrameStateChangeEvent += this.ResponseToFrameStateChange;
    }
}
