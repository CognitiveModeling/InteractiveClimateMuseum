using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZenFulcrum.EmbeddedBrowser
{

    /// <summary>
    /// A BrowserUI that tracks pointer interaction through a camera to a mesh of some sort.
    /// </summary>
    [RequireComponent(typeof(MeshCollider))]
    public class PointerUIMesh : PointerUIBase
    {
        protected MeshCollider meshCollider;

        protected Dictionary<int, RaycastHit> rayHits = new Dictionary<int, RaycastHit>();

        [Tooltip("Which layers should UI rays collide with (and be able to hit)?")]
        public LayerMask layerMask = -1;

        public override void Awake()
        {
            // call Awake() of the parent class PointerUIBase (browser is initialized and event for handling the pointers is registered)
            base.Awake();
            // initialize mesh collider
            meshCollider = GetComponent<MeshCollider>();
        }

        // overrides the abstract method from PointerUIBase
        // converts a 2D screen-space coordinate to browser-space coordinates
        protected override Vector2 MapPointerToBrowser(Vector2 screenPosition, int pointerId)
        {
            // if camera gets a view, use it, else use main camera
            var camera = viewCamera ? viewCamera : Camera.main;
            // if there is no camera: throw error, set mouse input boolean to false and return NaN vector
            if (!camera)
            {
                Debug.LogError("No main camera and no viewCamera specified. We can't map screen-space mouse clicks to the browser without a camera.", this);
                enableMouseInput = false;
                return new Vector2(float.NaN, float.NaN);
            }
            // if camera is found: call MapRayToBrowser() below with the screen pointer transformed to a ray
            return MapRayToBrowser(camera.ScreenPointToRay(screenPosition), pointerId);
        }

        // overrides the abstract method from PointerUIBase
        // converts a 3D world-space ray to a browser-space coordinate
        protected override Vector2 MapRayToBrowser(Ray worldRay, int pointerId)
        {
            // define a hit and fill it with the clostest collider that is hit (if any), ray starting from given worldspace and using layerMask
            RaycastHit hit;
            var rayHit = Physics.Raycast(worldRay, out hit, maxDistance, layerMask);

            //store hit data for GetCurrentHitLocation in the dictionary rayHits
            rayHits[pointerId] = hit;

            // if nothing was hit or the hit collider is not the same as the current one (check via mesh collider)
            if (!rayHit || hit.collider.transform != meshCollider.transform)
            {
                //not aimed at it, return Nan vector
                return new Vector3(float.NaN, float.NaN);
            }
            // else: return the texture coordinates of the hit collider
            else
            {
                return hit.textureCoord;
            }
        }

        // determines current hit location
        public override void GetCurrentHitLocation(out Vector3 pos, out Quaternion rot)
        {
            // if there is no pointer, return
            if (currentPointerId == 0)
            {
                //no pointer
                pos = new Vector3(float.NaN, float.NaN, float.NaN);
                rot = Quaternion.identity;
                return;
            }

            // take information about hit collider out of dictionary
            var hitInfo = rayHits[currentPointerId];

            // determine the a vector that defines in which direction is "up"
            //We need to know which way is up, so the cursor has the correct "up".
            //There's a couple ways to do this:
            //1. Use the barycentric coordinates and some math to figure out what direction the collider's
            //  v (from the uv) is getting bigger/smaller, then do some math to find out what direction
            //  that is in world space.
            //2. Just use the collider's local orientation's up. This isn't accurate on highly
            //  distorted meshes, but is much simpler to calculate.
            //For now, we use method 2.
            var up = hitInfo.collider.transform.up;

            // set position to point in world space where ray hit collider
            pos = hitInfo.point;
            // rotate to the negative normal of the surface the ray hit, using the "up" defined before
            rot = Quaternion.LookRotation(-hitInfo.normal, up);
        }

    }

}
