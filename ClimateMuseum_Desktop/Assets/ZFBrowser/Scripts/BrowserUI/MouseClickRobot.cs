using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class MouseClickRobot : MonoBehaviour
{
    public enum PROXY_TYPE
    {
        NONE,
        INITIAL,

        CENTER_IMAGE_EXPAND,
        CENTER_IMAGE_EXTRACT,

        COAL,
        RENEWABLES,
        OIL,
        NUCLEAR,
        NATURAL_GAS,
        ZERO_CARBON,
        BIOENERGY,
        CARBON_PRICE,
        TRANSPORT_ENERGY_EFFICIENCY,
        TRANSPORT_ELECTRIFICATION,
        BUILDINGS_ENERGY_EFFICIENCY,
        BUILDINGS_ELECTRIFICATION,
        GROWTH_POPULATION,
        GROWTH_ECONOMY,
        DEFORESTATION,
        METHANE,
        AFFORESTATION,
        TECHNOLOGICAL,

        GRAPH,
        IMPACT_GRAPH,
        TEMP_GRAPH,
        CO2_GRAPH,
        GREENHOUSE_GRAPH,
        SEA_LEVEL_GRAPH,
        OCEAN_ACIDIFICATION_GRAPH,
        AIR_POLLUTION_GRAPH,
        CROP_GRAPH
    }
    
    public PROXY_TYPE proxyType;

    public float MinX = -1.0f;

    public float MaxX = -1.0f;

    private float initialX;

    void Start()
    {
        this.initialX = this.transform.localPosition.x;
        SliderEventSystem.aSimulatorSliderEvent += this.setPercentageExternal;
    }

   
    public void setPercentage(float percentage)
    {
        if (this.MaxX != -1.0f && this.MinX != -1.0f)
        {
            this.transform.localPosition = new Vector3(this.MinX + (this.MaxX - this.MinX) * percentage, this.transform.localPosition.y, this.transform.localPosition.z);
            if (this.transform.localPosition.x < this.MinX)
            {
                this.transform.localPosition = new Vector3(this.MinX, this.transform.localPosition.y, this.transform.localPosition.z);
            }
            if (this.transform.localPosition.x > this.MaxX)
            {
                this.transform.localPosition = new Vector3(this.MaxX, this.transform.localPosition.y, this.transform.localPosition.z);
            }
            Debug.LogWarning(this.proxyType + ":" + percentage);
        }
    }

    public void setPercentageExternal(float percentage, MouseClickRobot.PROXY_TYPE type)
    {
        if (this.proxyType == type)
        {
            this.setPercentage(percentage);
        }
    }
}
