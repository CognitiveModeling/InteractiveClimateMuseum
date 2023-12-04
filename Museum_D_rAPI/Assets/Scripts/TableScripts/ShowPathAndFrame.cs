using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the orientation with the interactive table.
// If the user clicks onto a specific content card on the table (Afforestation, Technological Carbon Removal, ...),
// a frame (dis)appears around it and a path to the corresponding panel (dis)appears.
// The script is assigned to the specific Content Cards on the interactive Table in the editor.

public class ShowPathAndFrame : MonoBehaviour
{
    // a path and a frame around the content card,
    // both assigned in the editor ("Table -> Paths" and "Table -> Content Card -> [Specific Content Card Name] -> Frame")
    public GameObject Path;
    public GameObject Frame;

    // if user clicks on the content card, a frame will appear/disappear around it and a path to the corresponding panel will appear/disappear
    void OnMouseDown()
    {
        // if frame is not active
        if (!(Frame.activeSelf))
 	    {
            // collect all content cards' frames, also correlation frames
            GameObject[] tableCardFrames;
            tableCardFrames = GameObject.FindGameObjectsWithTag("Table Card Frame");

            // collect all paths
            GameObject[] paths;
            paths = GameObject.FindGameObjectsWithTag("Path");

            // deactivate all frames, also correlation frames
            foreach (GameObject frame in tableCardFrames)
            {
                frame.SetActive(false);
            }

            // deactivate all paths
            foreach (GameObject path in paths)
            {
                path.SetActive(false);
            }

            // reactivate only the path and frame that corresponds to the specific content card that was clicked
            Path.SetActive(true);
            Frame.SetActive(true);
        }

        // if path and frame is already there, deactivate both
        else
 	    {
            Path.SetActive(false);
            Frame.SetActive(false);
        }
    }
}
