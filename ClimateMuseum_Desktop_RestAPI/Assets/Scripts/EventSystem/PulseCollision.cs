﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EventSystemBase.aCollisionEvent("pulse");
    }
}
