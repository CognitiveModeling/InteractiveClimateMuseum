using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEventSystem : MonoBehaviour
{
    public delegate void SimulatorSliderEvent(float percentage, MouseClickRobot.PROXY_TYPE type);
    public static SimulatorSliderEvent aSimulatorSliderEvent;
}
