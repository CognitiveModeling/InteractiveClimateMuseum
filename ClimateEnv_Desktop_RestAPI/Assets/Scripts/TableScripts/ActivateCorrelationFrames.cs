using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script activates the frames around the Content Cards and sub topics that correlate to the clicked Content Card at the interactive table.
// It is assigned to each Content Card (Carbon Removal, ...) and their sub topics (Afforestation, ...) at the Table in the editor.

public class ActivateCorrelationFrames : MonoBehaviour
{   
    // a new dictionary of correlations between Content Cards and their sub topics (s. script CorrelationDictionary)
    public static CorrelationDictionary correlations = new CorrelationDictionary();

    // delegate method describing which content card is changed (on/off)
    public delegate void OnFrameStateChange(string tableCardName);

    // event describing the state of a frame (on/off)
    public static event OnFrameStateChange OnFrameStateChangeEvent;

    // if user clicks on Content Card or sub topic, event is provoked and state of correlative Content Card is changed (on/off)
    public void OnMouseDown()
    {
        OnFrameStateChangeEvent(this.gameObject.name);
    }

    // listens to event of a state change of the frame and calls the Response function below
    void Start()
    {
        ActivateCorrelationFrames.OnFrameStateChangeEvent += this.ResponseToFrameStateChange;
    }


    // as response to frame state change, frame of deactivated correlative Content Cards are activated and active ones are deactivated
    private void ResponseToFrameStateChange(string correlationTableCardName)
    {
        // define an empty array of strings
        string[] correlationsArray = null;

        //  if the clicked Content Card is found in the dictionary of correlations,
        // fill array iteratively with correlative Content Card names and return true if a correlative was found, else false
        if (ActivateCorrelationFrames.correlations.correlationDictionary.TryGetValue(this.gameObject.name, out correlationsArray))
        {
            // loop through all correlative Content Cards in this array
            foreach(string correlation in correlationsArray)
            {
                // if entry name corresponds to the input name
                if (correlation == correlationTableCardName)
                {
                    // define a frame object (s. child "Frame Correlate" of Content Card in editor)
                    GameObject correlationFrame = this.transform.Find("Frame Correlate").gameObject;

                    // if frame is not active, it is activated
                    if (correlationFrame.activeSelf == false)
                    {
                        correlationFrame.SetActive(true);
                    }
                    
                    // if frame is active, it is deactivated
                    else
                    {
                        correlationFrame.SetActive(false);
                    } 
                }
            }
        }
    }
}
