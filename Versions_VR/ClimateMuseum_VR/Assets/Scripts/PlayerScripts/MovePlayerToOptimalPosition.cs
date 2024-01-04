using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

// This script moves the player to the optimal position in front of a panel if user clicks at the green button on the floor.
// It is assigned to each Floor Position in front of a panel in the editor.

public class MovePlayerToOptimalPosition : MonoBehaviour
{
    public GameObject player;
    public GameObject panel;

    // additional for VR:
    void Start()
    {
        // if the Floor Position does not have a collider yet
        if (!this.gameObject.GetComponent<Collider>())
        {
            // add a sphere collider with a trigger
            this.gameObject.AddComponent<SphereCollider>();
            this.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }

        // listen for events of the Vive controllers
        SteamVR_LaserPointer.PointerClick += this.HandleVivePointerEvent;
    }

    void OnMouseDown()
    {
        // if VR version is running
        if (GameObject.FindGameObjectWithTag("VRPlayerRoot"))
        {
            // get VR player
            Player player = Valve.VR.InteractionSystem.Player.instance;

            // set player's position to the position of the green Floor Position tile (regarding feet offset)
            Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position = this.transform.position + playerFeetOffset;

            // if player has objects attached to its hands, reset attachment
            if (player.leftHand.currentAttachedObjectInfo.HasValue)
                player.leftHand.ResetAttachedTransform(player.leftHand.currentAttachedObjectInfo.Value);
            if (player.rightHand.currentAttachedObjectInfo.HasValue)
                player.rightHand.ResetAttachedTransform(player.rightHand.currentAttachedObjectInfo.Value);
        }

        // if Desktop Version is running
        else
        {
            // move player to Floor position
            Vector3 newPosition = player.transform.position;
            newPosition.z = this.transform.position.z;
            newPosition.x = this.transform.position.x;
            player.transform.position = newPosition;

            // rotate player and its camera
            player.transform.rotation = panel.transform.rotation;
            player.transform.Find("Camera").transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // additional for VR: a public method for calling OnMouseDown() from other scripts
    public void CallOnMouseDown()
    {
        this.OnMouseDown();
    }

    // additional for VR:
    private void HandleVivePointerEvent(object sender, PointerEventArgs e)
    {
        // if an event from the VR controllers comes in and its target is the current Floor Position where this script is assigned to, call OnMouseDown
        if (e.target == this.transform)
        {
            this.OnMouseDown();
        }
    }
}