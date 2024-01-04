using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EventSystemBase.aCollisionEvent("rotation");
    }
}
