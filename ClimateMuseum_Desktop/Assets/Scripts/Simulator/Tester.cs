using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public CMDInterface cmdInterface;
    public SimulationRenderer simulationRenderer;

    public bool correctTemperature = false;
    string url;

    [SerializeField] private Material cube;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(cmdInterface.getTemp2100("6:110 7:2050"));
    }

    void Update()
    {
        if (correctTemperature)
        {
            cube.color = new Color(0.364f, 0.925f, 0.160f, 1f);
        }
        url = simulationRenderer.url;
        UnityEngine.Debug.Log("URL:" + url);
    }
}
