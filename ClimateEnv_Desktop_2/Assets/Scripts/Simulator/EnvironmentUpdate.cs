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
using System.Security;

public class EnvironmentUpdate : MonoBehaviour
{
    // All Debug.Log/LogWarning messages are placed in comments, so they don�t affect the running time, for programming it�s best to uncomment them (selectively)

    //public Browser browser;

    public Gradients gradients;
    public GetSliderValues getSliderValues;
    public CMDinterface cmdInterface;
    public SimulationRenderer simulationRenderer;
    public Set2100 set2100;

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
    [SerializeField] private Material tree4;
    [SerializeField] private Material tree5;
    [SerializeField] private Material tree6;
    [SerializeField] private Material tree7;
    [SerializeField] private Material tree0;
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
        set2100.set2100();
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
            upper = 5.0f;

            tree1.color = gradients.gradientTree1.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.7)
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
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree3.color = new Color(tree3.color.r, tree3.color.g, tree3.color.b, 0f);
            }
            tree4.color = gradients.gradientTree4.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree4.color = new Color(tree4.color.r, tree4.color.g, tree4.color.b, 0f);
            }
            tree5.color = gradients.gradientTree5.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree5.color = new Color(tree5.color.r, tree5.color.g, tree5.color.b, 0f);
            }
            tree6.color = gradients.gradientTree6.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree6.color = new Color(tree6.color.r, tree6.color.g, tree6.color.b, 0f);
            }
            tree7.color = gradients.gradientTree7.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree7.color = new Color(tree7.color.r, tree7.color.g, tree7.color.b, 0f);
            }
            tree0.color = gradients.gradientTree0.Evaluate(valToPerc(lower, upper, prognosis));
            // make leaves disappear if prognosis >= 85% of worst case value:
            if (valToPerc(lower, upper, prognosis) >= 0.85)
            {
                tree0.color = new Color(tree0.color.r, tree0.color.g, tree0.color.b, 0f);
            }

            ground.color = gradients.gradientGround.Evaluate(valToPerc(lower, upper, prognosis));
            mountain.color = gradients.gradientMountain.Evaluate(valToPerc(lower, upper, prognosis));
            posLake = objLake.transform.position;

            float lakeMax = -1f;
            float lakeMin = -20f;

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

            float oceanMax = -1.0f;
            float oceanMin = -22f;
            yOcean = oceanMin + ((-1f) * valToPerc(lower, upper, prognosis) * (oceanMin - oceanMax));
            posOcean.y = yOcean;
            objOcean.transform.position = posOcean;

            posIceberg = objIceberg.transform.position;

            float icebergMax = -40.0f;
            float icebergMin = -100.0f;
            yIceberg = icebergMax - ((-1f) * valToPerc(lower, upper, prognosis) * (icebergMin - icebergMax));
            posIceberg.y = yIceberg;
            objIceberg.transform.position = posIceberg;

            //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + prognosis);

            ocean.color = gradients.gradientOcean.Evaluate(valToPerc(lower, upper, prognosis));

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
        tree1.color = new Color(0.46666667f, 0.72549020f, 0.23921569f, 1f);
        tree2.color = new Color(0.45490196f, 0.85098039f, 0.20784314f, 1f);
        tree3.color = new Color(0.23137255f, 0.68235294f, 0.48627451f, 1f);
        tree4.color = new Color(0.36862745f, 0.56862745f, 0.42352941f, 1f);
        tree5.color = new Color(0.24705882f, 0.67058824f, 0.40392157f, 1f);
        tree6.color = new Color(0.10980392f, 0.70588235f, 0.10980392f, 1f);
        tree7.color = new Color(0.41176471f, 0.76470588f, 0.28627451f, 1f);
        tree0.color = new Color(0.21568627f, 0.55686275f, 0.21568627f, 1f);

        ground.color = new Color(0.19215686f, 0.75294118f, 0.14901961f, 1f);
        mountain.color = new Color(0.29411765f, 0.18431373f, 0.00000000f, 1f);

        posLake = objLake.transform.position;
        yLake = -2f;
        posLake.y = yLake;
        objLake.transform.position = posLake;

        ParticleSystem.MainModule cloudsMain = psClouds.main;
        cloudsMain.maxParticles = 3;
        clouds.color = new Color(1f, 1f, 1f, 1f);

        Color skyboxColor = new Color(0f, 0.6698113f, 0.6501371f, 1f);
        RenderSettings.skybox.SetColor("_SkyTint", skyboxColor);

        posOcean = objOcean.transform.position;
        yOcean = -27f;
        posOcean.y = yOcean;
        objOcean.transform.position = posOcean;

        posIceberg = objIceberg.transform.position;
        yIceberg = -27.63549f;
        posIceberg.y = yIceberg;
        objIceberg.transform.position = posIceberg;

        //Color oceanColor = new Color(0.07029124f, 0.4899847f, 0.7828243f, 1f);
        ocean.color = new Color(0.25098039f, 0.82352941f, 1.0f, 1f);

        ParticleSystem.MainModule smogMain = psSmog.main;
        smogMain.maxParticles = 0;
        smog.color = new Color(1f, 1f, 1f, 0f);
    }

}