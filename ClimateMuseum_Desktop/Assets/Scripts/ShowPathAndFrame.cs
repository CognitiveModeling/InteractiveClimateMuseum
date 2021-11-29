using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPathAndFrame : MonoBehaviour
{
public GameObject Path;
public GameObject Frame;

    void OnMouseDown ()
    {
        if (!(Frame.activeSelf))
 	    {    
            GameObject[] tableCardFrames;
            tableCardFrames = GameObject.FindGameObjectsWithTag("Table Card Frame");

            GameObject[] paths;
            paths = GameObject.FindGameObjectsWithTag("Path");

            foreach (GameObject frame in tableCardFrames)
            {
                frame.SetActive(false); //deactivate all frames, also correlation frames
            }

            foreach (GameObject path in paths)
            {
                path.SetActive(false);
            }

            Path.SetActive(true);
            Frame.SetActive(true);
        }

        else
 	    {
            Path.SetActive(false);
            Frame.SetActive(false);

        }

    }
}
