using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script checks the teleportation state if the player moves through the door of the Simulator room.
// Thus, it is assigned to the object TeleportCheck of the Simulator room's door frame in the editor.

public class TeleportCheck : MonoBehaviour
{
    // if the player moves through the door (TeleportCheck) of the Simulator room, set the teleportation state to false
    private void OnTriggerEnter(Collider other)
    {
        // if the teleportation already happened and thus, the state is true:
        if (other.gameObject.GetComponent<Teleport>())
        {
            // set the teleportation state to false so that the next teleportation can happen
            other.gameObject.GetComponent<Teleport>().teleported = false;
        }
    }
}
