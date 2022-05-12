using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemBase : MonoBehaviour
{
    public delegate void CollisionEvent(string type);
    public static CollisionEvent aCollisionEvent;
}
