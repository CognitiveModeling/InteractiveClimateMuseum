using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script manages the player's teleportation to the target (the full-version simulator)
// and back to the original position where the player stood before this teleportation.
// It is assigned to the Player in the editor.

public class Teleport : MonoBehaviour
{
    // variables needed for teleportation:
    // a teleportation target (position in front of the full-version simulator)
    public Transform target;
    // the player's original position before the teleportation to the simulator takes place
    public Vector3 oldPos;
    // a teleportation state (true if teleportation to target happened, false if teleportation back to original position happened)
    public bool teleported = false;
    // the camera attached to the player
    public GameObject playerCamera;
    // the player's original rotation before the teleportation to the simulator takes place
    public Vector3 oldRot;

    // variables needed for fade-in and -out:
    // a canvas, object can also be found in editor as "Fader Canvas"
    public GameObject canvas;
    // a Fader, object can also be found in editor as "Fader"
    public GameObject Fader;
    // an animator for realizing the scene's fading in/out during a teleportation
    private Animator anim;

    private void Awake()
    {
        // initializes the animator as a component of the Fader
        anim = Fader.GetComponent<Animator>();
    }

    void Update()
    {
        // if user presses "Return", scene fades out
        // and player is teleported to full-version simulator or back to original position before teleportation
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            // Fader Canvas is activated
            activateCanvas();

            // if player has not been teleported to the simulator yet (teleportation state is false),
            // it happens now
            if (!teleported)
            {
                // player's original position is saved
                oldPos = this.gameObject.transform.position;
                oldRot = this.gameObject.transform.localRotation.eulerAngles;

                // scene fades out
                FadeOut();
                
                // performs teleportation to target and scene's fading-in with small temporal delay 
                Invoke("GoToDestination", 0.9f);
                Invoke("FadeIn", 0.7f);
            }

            // if player has already been teleported to the simulator (teleportation state is true),
            // player is teleported back to original position
            else
            {
                // scene fades out
                FadeOut();
                
                // performs teleportation to original position and scene's fading-in with small temporal delay 
                Invoke("GoToOldPos", 0.9f);
                Invoke("FadeIn", 0.7f);
            }

            // Fader Canvas is deactivated with temporal delay
            Invoke("deactivateCanvas", 1.0f);
        }
    }

    /*
     * some auxiliary methods
    */

    // method for activating the Fader Canvas
    void activateCanvas()
    {
        canvas.SetActive(true);
    }

    // method for deactivating the Fader Canvas
    void deactivateCanvas()
    {
        canvas.SetActive(false);
    }

    // method for scene's fade-out, uses Fader's Animator anim
    void FadeOut()
    {
        anim.SetBool("FadeIn", false);
        anim.SetBool("FadeOut", true);
    }

    // method for scene's fade-in, uses Fader's Animator anim
    void FadeIn()
    {
        anim.SetBool("FadeOut", false);
        anim.SetBool("FadeIn", true);
    }

    // method for changing the player's position to the target position (full-version simulator) and facing player and its camera towards panel
    void GoToDestination()
    { 
        this.gameObject.transform.position = new Vector3(target.transform.position.x, 0.6f, target.transform.position.z);
        this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        this.gameObject.transform.Find("Camera").transform.localRotation = Quaternion.Euler(0, 0, 0);
        teleported = true;
    }

    // method for changing the player's position to the original position before player was teleported to the simulator
    void GoToOldPos()
    {
        this.gameObject.transform.position = oldPos;
        this.gameObject.transform.rotation = Quaternion.Euler(oldRot);
        teleported = false;
    }
}