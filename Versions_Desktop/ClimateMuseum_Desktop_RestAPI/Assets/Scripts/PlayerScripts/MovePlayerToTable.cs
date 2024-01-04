using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script moves the player to the optimal position in front of a panel if user clicks at the green button on the floor.
// It is assigned to each Floor Position in front of a panel in the editor.

public class MovePlayerToTable : MonoBehaviour
{
    // a player and a panel, both are assigned in the editor
    public GameObject player;
    //public GameObject panel;

    // method is called if user clicks on the optimal position in front of a panel (green button on the floor)
    void OnMouseDown()
    {
        // set player to x- and z-coordinate of the Floor Position
        player.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);

        // player and player camera are rotated towards the panel
        player.transform.rotation = Quaternion.Euler(0, 360, 0);
        player.transform.Find("Camera").transform.localRotation = Quaternion.Euler(40, 0, 0);
    }
}
