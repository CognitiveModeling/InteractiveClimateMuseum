using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EventSystemBase.aCollisionEvent("color");
    }
}
