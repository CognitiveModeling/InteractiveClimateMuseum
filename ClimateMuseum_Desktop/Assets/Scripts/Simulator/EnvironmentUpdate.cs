using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using System.Linq;
using System.Diagnostics;
using System.Security.Policy;


public class EnvironmentUpdate : MonoBehaviour
{
    // All Debug.Log/LogWarning messages are placed in comments, so they don�t affect the running time, for programming it�s best to uncomment them (selectively)

    //public Browser browser;

    public Gradients gradients;
    public GetSliderValues getSliderValues;
    public CMDinterface cmdInterface;
    public SimulationRenderer simulationRenderer;

    public bool busy = false; // set to true while the changes of the sliders are applied to hidden browsers and the environment is changed
    public bool active2100 = false;

    public GameObject loadingCircles; // will be activated whilst the environment doesn�t match the slider values yet

    private String lastURL; 
    private String currURL;
    private String sliderValues;
    private float prognosis;

    // upper and lower will be used as boundaries of several different value ranges
    private float upper;
    private float lower;

    // materials that will be changed by the script
    [SerializeField] private Material tree1;
    [SerializeField] private Material tree2;
    [SerializeField] private Material tree3;
    [SerializeField] private Material ocean;
    [SerializeField] private Material lake;
    [SerializeField] private Material ground;
    [SerializeField] private Material mountain;
    [SerializeField] private Material clouds;
    [SerializeField] private Material smog;
    [SerializeField] private Material skybox;

    // objects that will be moved in the script
    public GameObject objOcean;
    public GameObject objLake;
    public GameObject objIceberg;
    private Vector3 posOcean;
    private Vector3 posLake;
    private Vector3 posIceberg;
    private float yOcean;
    private float yLake;
    private float yIceberg;

    // particle systems for the clouds/smog
    [SerializeField] private ParticleSystem psClouds;
    [SerializeField] private ParticleSystem psSmog;

    //Start is called before the first frame update
    void Start()
    {
        set2022();
        this.lastURL = simulationRenderer.url;
        this.currURL = simulationRenderer.url;
    }

    // Update is called once per frame
    // checks if adaptions are still being applied (busy=True) or if potential slider values should be extracted and applied to the environment
    void Update()
    {
        this.currURL = simulationRenderer.url; //set to current URL
        if (!this.busy && checkForChange(this.lastURL, this.currURL)) // prevents executions whilst previous adaptions are still made
        {
            //ROAAstartTime = Time.time; // for debugging purposes (to measure duration of readOutAndApplyValues())
            this.busy = true; // prevents parallel runs of the routine
            StartCoroutine(this.readOutAndApplyValues());
        }
    }

    public bool checkForChange(string lastURL, string currURL)
    {
        if (lastURL == null || currURL == null)
        {
            UnityEngine.Debug.LogWarning("URL is null.");
            return false;
        }

        this.lastURL = this.currURL;
        return !lastURL.Equals(currURL);
    }

    // reads out the slider values, applies them to the hidden SyncBrowser, copies the predictions and applies them to the environment
    private IEnumerator readOutAndApplyValues()
    {
        sliderValues = getSliderValues.getter(currURL); //turn URL to one string that complies to the format required by the API
        //UnityEngine.Debug.LogWarning("Curr URL: " + currURL);
        //UnityEngine.Debug.LogWarning("sliderValues: " + sliderValues);

        if (sliderValues.Length > 0)
        {
            cmdInterface.getTemp2100(sliderValues); //feed sliderValues to API, retreive temp(2100) prognosis, apply this value to the environment
        }
        else
        {
            cmdInterface.getTemp2100("1:0");
        }
        yield return null;
    }

    public void apply(float temp2100)
    {
        // prevents confusion with the separators
        var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator = ".";

        // -------------- Applies the prognosis to the environment -------------- 
        if (active2100)
        {
            // TEMPERATURE affects tree color, ground color, mountain color and lake height
            //baselineTemperature = baseline; 
            // saves baseline values for resetting
            // meaning of lower and upper for all the following cases already explained in resetMaterials()
            prognosis = temp2100;
            lower = 0.9f;
            upper = 5.8f;

            tree1.color = gradients.gradientTree1.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree1.color = new Color(tree1.color.r, tree1.color.g, tree1.color.b, 0f);
            }
            tree2.color = gradients.gradientTree2.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 80% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.80)
            {
                tree2.color = new Color(tree2.color.r, tree2.color.g, tree2.color.b, 0f);
            }
            tree3.color = gradients.gradientTree3.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 70% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.70)
            {
                tree3.color = new Color(tree3.color.r, tree3.color.g, tree3.color.b, 0f);
            }

            ground.color = gradients.gradientGround.Evaluate(valToPerc(lower, upper, prognosis));
            mountain.color = gradients.gradientMountain.Evaluate(valToPerc(lower, upper, prognosis));
            posLake = objLake.transform.position;

            float lakeMax = -5.01f;
            float lakeMin = -8f;

            yLake = lakeMax - ((-1f) * valToPerc(lower, upper, prognosis) * (lakeMin - lakeMax));
            posLake.y = yLake;
            objLake.transform.position = posLake;
            //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + prognosis);

            // CO2-CONCENTRATION affects cloud color and concentration

            //baselineCO2 = baseline;
            ParticleSystem.MainModule cloudsMain = psClouds.main;
            cloudsMain.maxParticles = (int)(valToPerc(lower, upper, prognosis) * 140);
            clouds.color = gradients.gradientClouds.Evaluate(valToPerc(lower, upper, prognosis));
            //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + prognosis);

            RenderSettings.skybox.SetColor("_SkyTint", gradients.gradientSkybox.Evaluate(valToPerc(lower, upper, prognosis)));
            //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + prognosis);

            posOcean = objOcean.transform.position;

            float oceanMax = -2f;
            float oceanMin = -15.5f;
            yOcean = oceanMin + ((-1f) * valToPerc(lower, upper, prognosis) * (oceanMin - oceanMax));
            posOcean.y = yOcean;
            objOcean.transform.position = posOcean;

            posIceberg = objIceberg.transform.position;

            float icebergMax = -15f;
            float icebergMin = -125f;
            yIceberg = icebergMax - ((-1f) * valToPerc(lower, upper, prognosis) * (icebergMin - icebergMax));
            posIceberg.y = yIceberg;
            objIceberg.transform.position = posIceberg;

            //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + prognosis);

            ocean.SetColor("_BaseColor", gradients.gradientOcean.Evaluate(valToPerc(lower, upper, prognosis)));

            ParticleSystem.MainModule smogMain = psSmog.main;
            smogMain.maxParticles = (int)(valToPerc(lower, upper, prognosis) * 140);
            smog.color = gradients.gradientSmog.Evaluate(valToPerc(lower, upper, prognosis));

        }

        loadingCircles.SetActive(false); // deactivates loading circle
        this.busy = false;
    }


    // takes upper and lower limit of a range and a value and returns a float between 0 and 1 that indicates the position between these limits
    // will be used for mapping value of graph data to the color gradient/height
    private float valToPerc(float lower, float upper, float val)
    {
        if (val < lower)
        {
            return 0.0f; // if smaller than minimum, evaluate to 0
        }
        else if (val > upper)
        {
            return 1.0f; // if higher than maximum, evaluate to 1
        }
        else
        {
            float hundrPerc = (upper - lower);
            float res = (val - lower) / hundrPerc;
            return res; // returns float between 0f and 1f
        }
    }

    // -------------------- Public Getter and Setter -------------------- 
    public void startReadOutAndApplyValues()
    {
        this.currURL = simulationRenderer.url;
        StartCoroutine(this.readOutAndApplyValues());
    }

    public void set2022()
    {
        tree1.color = new Color(0.364f, 0.925f, 0.160f, 1f);
        tree2.color = new Color(0.231f, 0.639f, 0.082f, 1f);
        tree3.color = new Color(0.027f, 0.380f, 0.015f, 1f);

        ground.color = new Color(0.2f, 0.603f, 0f, 1f);
        mountain.color = new Color(0.294f, 0.188f, 0f, 1f);

        posLake = objLake.transform.position;
        yLake = -5.293135f;
        posLake.y = yLake;
        objLake.transform.position = posLake;

        ParticleSystem.MainModule cloudsMain = psClouds.main;
        cloudsMain.maxParticles = 3;
        clouds.color = new Color(1f, 1f, 1f, 1f);

        Color skyboxColor = new Color(0f, 0.6698113f, 0.6501371f, 1f);
        RenderSettings.skybox.SetColor("_SkyTint", skyboxColor);

        posOcean = objOcean.transform.position;
        yOcean = -20f;
        posOcean.y = yOcean;
        objOcean.transform.position = posOcean;

        posIceberg = objIceberg.transform.position;
        yIceberg = -27.63549f;
        posIceberg.y = yIceberg;
        objIceberg.transform.position = posIceberg;

        Color oceanColor = new Color(0.07029124f, 0.4899847f, 0.7828243f, 1f);
        ocean.SetColor("_BaseColor", oceanColor);

        ParticleSystem.MainModule smogMain = psSmog.main;
        smogMain.maxParticles = 0;
        smog.color = new Color(1f, 1f, 1f, 1f);
    }

}
