#if UNITY_2017_2_OR_NEWER

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using ZenFulcrum.EmbeddedBrowser.VR;

namespace ZenFulcrum.EmbeddedBrowser
{

    /// <summary>
    /// Class for tracking (and optionally rendering) a tracked controller, usable for VR input to a browser.
    ///
    /// Put two VRHand objects in the scene, one for each controller. Make sure they have the same transform parent
    /// as the main camera so they will correctly move with the player.
    /// (Also, make sure your main camera is centered on its local origin to start.)
    ///
    /// If desired, you can also put some visible geometry under the VRHand. Set it as `visualization` and it will
    /// move with the controller and disappear when untracked.
    ///
    /// PointerUIBase.FeedVRPointers will find us and read our state out to browsers.
    /// </summary>
    public class VRBrowserHand : MonoBehaviour
    {

        [Tooltip("Which hand we should look to track.")]
        public XRNode hand = XRNode.LeftHand;

        [Tooltip("Optional visualization of this hand. It should be a child of the VRHand object and will be set active when the controller is tracking.")]
        public GameObject visualization;


        [Tooltip("How much we must slide a finger/joystick before we start scrolling.")]
        public float scrollThreshold = .1f;

        [Tooltip(@"How fast the page moves as we move our finger across the touchpad. Set to a negative number to enable that infernal ""natural scrolling"" that's been making so many trackpads unusable lately.")]
        public float trackpadScrollSpeed = .05f;
        [Tooltip("How fast the page moves as we scroll with a joystick.")]
        public float joystickScrollSpeed = 75f;

        private Vector2 lastTouchPoint;
        private bool touchIsScrolling;

        /// <summary>
        /// Are we currently tracking?
        /// </summary>
        public bool Tracked { get; private set; }

        /// <summary>
        /// Currently depressed buttons.
        /// </summary>
        public MouseButton DepressedButtons { get; private set; }

        /// <summary>
        /// How much we've scrolled since the last frame. Same units as Input.mouseScrollDelta.
        /// </summary>
        public Vector2 ScrollDelta { get; private set; }

        private XRNodeState nodeState;
        private VRInput input;

        public float LeftClickAmplitude;
        public float MiddleClickAmplitude;
        public float RightClickAmplitude;

        public void OnEnable()
        {
            // initialize VR input
            VRInput.Init();
            input = VRInput.Impl;

            // VR poses update after LateUpdate and before OnPreCull, event is registered by camera
            // onPreCull is an event function that unity calls before a camera culls the scene. (https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnPreCull.html)
            Camera.onPreCull += UpdatePreCull;

            // if visualization of the hand is active, deactivate it
            if (visualization) visualization.SetActive(false);
        }

        public void OnDisable()
        {
            // ReSharper disable once DelegateSubtraction, event is no longer registered by camera
            Camera.onPreCull -= UpdatePreCull;
        }

        public virtual void Update()
        {
            if (Time.frameCount < 5) return; // give the SteamVR SDK a chance to start up
            ReadInput();
        }

        protected virtual void ReadInput()
        {
            // depressed buttons and scroll delta is set to 0
            DepressedButtons = 0;
            ScrollDelta = Vector2.zero;

            // if the left (/ middle / right) click amplitude is greater than 0.9 (/0.5),
            // the number of depressed buttons and the left (/ middle / right) mouse button are compared bitwise
            if (LeftClickAmplitude > .9f) DepressedButtons |= MouseButton.Left;
            if (MiddleClickAmplitude > .5f) DepressedButtons |= MouseButton.Middle;
            if (RightClickAmplitude > .5f) DepressedButtons |= MouseButton.Right;

            // boolean Tracked is set to the tracking state of the XRNodeState
            this.Tracked = nodeState.tracked;

            /* 02.11.21: changed the readout, it is now forwarded externally
            if (!nodeState.tracked) return;

            var leftClick = input.GetAxis(nodeState, InputAxis.LeftClick);
            if (leftClick > .9f) DepressedButtons |= MouseButton.Left;

            var middleClick = input.GetAxis(nodeState, InputAxis.MiddleClick);
            if (middleClick > .5f) DepressedButtons |= MouseButton.Middle;

            var rightClick = input.GetAxis(nodeState, InputAxis.RightClick);
            if (rightClick > .5f) DepressedButtons |= MouseButton.Right;


            var joyTypes = input.GetJoypadTypes(nodeState);
            if ((joyTypes & JoyPadType.Joystick) != 0) ReadJoystick();
            if ((joyTypes & JoyPadType.TouchPad) != 0) ReadTouchpad();
            */

            // amplitude of left, middle and right click is reported (see console)
            Debug.Log(LeftClickAmplitude + " : " + MiddleClickAmplitude + " : " + RightClickAmplitude);
        }

        protected virtual void ReadTouchpad()
        {
            // a 2-dimensional touchPoint with x- and y-coordinate of the touch pad
            var touchPoint = new Vector2(
                input.GetAxis(nodeState, InputAxis.TouchPadX), input.GetAxis(nodeState, InputAxis.TouchPadY)
            );

            // a boolean touchButton indicating if the axis between nodeState (XRNodeState) and the touch on the pad is greater than 0.5
            var touchButton = input.GetAxis(nodeState, InputAxis.TouchPadTouch) > .5f;

            // if touchButton is true
            if (touchButton)
            {
                // difference between last and current touch point is calculated
                var delta = touchPoint - lastTouchPoint;
                // if player is not scrolling yet
                if (!touchIsScrolling)
                {
                    // if delta's magnitude multiplied by the scroll speed of the pad is greater than the scroll threshold
                    if (delta.magnitude * trackpadScrollSpeed > scrollThreshold)
                    {
                        // player is scrolling
                        touchIsScrolling = true;
                        // the last touch point is updated to the current one
                        lastTouchPoint = touchPoint;
                    }
                    // if scroll threshold is not exceeded: do nothing
                    else
                    {
                        //don't start updating the touch point yet
                    }
                }
                // if player is already scrolling:
                else
                {
                    // scrollDelta is updated by adding the product of the delta and the scroll speed of the pad
                    ScrollDelta += new Vector2(-delta.x, delta.y) * trackpadScrollSpeed;
                    lastTouchPoint = touchPoint;
                }
            }
            // if touchButton is false
            else
            {
                // the last touch point is updated to the current one
                lastTouchPoint = touchPoint;
                // player is not scrolling
                touchIsScrolling = false;
            }
        }

        protected virtual void ReadJoystick()
        {
            // a 2-dimensional position with x- and y-coordinate of the joy stick
            var position = new Vector2(
                -input.GetAxis(nodeState, InputAxis.JoyStickX),
                input.GetAxis(nodeState, InputAxis.JoyStickY)
            );

            // if x-coordinate (y-coordinate) is greater than the scroll threshold, change its value (add/substract threshold), else set it to 0
            position.x = Mathf.Abs(position.x) > scrollThreshold ? position.x - Mathf.Sign(position.x) * scrollThreshold : 0;
            position.y = Mathf.Abs(position.y) > scrollThreshold ? position.y - Mathf.Sign(position.y) * scrollThreshold : 0;

            // changes position by multiplicaiton with its magnitude, the scroll speed of the joy stick and the time passed since the last frame
            position = position * position.magnitude * joystickScrollSpeed * Time.deltaTime;

            // scrollDelta is updated by adding the calculated position
            ScrollDelta += position;
        }

        private int lastFrame;
        private List<XRNodeState> states = new List<XRNodeState>();
        private bool hasTouchpad;

        private void UpdatePreCull(Camera cam)
        {
            // if we are still in the same frame, return
            if (lastFrame == Time.frameCount) return;
            
            // set lastFrame to current frame counter
            lastFrame = Time.frameCount;
            
            // get tracking states
            InputTracking.GetNodeStates(states);
            
            // for each state:
            for (int i = 0; i < states.Count; i++)
            {
                //Debug.Log("A thing: " + states[i].nodeType + " and " + InputTracking.GetNodeName(states[i].uniqueID));
                // if it is not the hand, continue
                if (states[i].nodeType != hand) continue;
                
                //  if it is the hand (hand was found)
                //Debug.Log("found: " + hand);
                nodeState = states[i];
                // get its pose and set it as local position
                var pose = input.GetPose(nodeState);
                transform.localPosition = pose.pos;

                // get its rotation and set it as local rotation
                //if we are in the hierarchy, we do not want to change the orientation
                transform.localRotation = Quaternion.identity;// pose.rot;

                // if the visualization of the hand is active, keep it activated if the current hand is tracked and deactivate it if it is not
                if (visualization) visualization.SetActive(Tracked = nodeState.tracked);
            }
        }
    }
}
#endif