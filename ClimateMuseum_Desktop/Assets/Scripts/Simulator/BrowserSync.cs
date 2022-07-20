using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;
using System.Linq;

public class BrowserSync : MonoBehaviour
{
    // All Debug.Log/LogWarning messages are placed in comments, so they don´t affect the running time, for programming it´s best to uncomment them (selectively)

    public Browser browser;
    public List<Browser> refBrowser;
    public List<PointerUIBase> pointerUIBases;

    public Gradients gradients;

    private Dictionary<string, float> sliderValues; // stores current value for each slider
    private Dictionary<string, float> prevSliderValues; // stores previous value for each slider (to check if change is nessecary)
    private Dictionary<string, MouseClickRobot> sliderProxies; // contains references to the mouseClickRobot for each SyncBrowser (6) for each slider
    private Dictionary<BrowserSync.GraphTypes, string> graphData; // stores the data fetched by clicking "copy data to clipboard" for an impact graph in the En-ROADS simulator
    private Dictionary<BrowserSync.GraphTypes, MouseClickRobot> graphProxies; // contains the references to the mouseClickRobots for the graph manipulation

    private bool busy = false; // set to true while the changes of the sliders are applied to hidden browsers and the environment is changed
    private bool quit = false; // set to true once the museum is exited (via the escape button or the "Quit Museum" Button)
    private bool firstTime = true; // set to false once the initialisation is over)
    private bool hasChanged = false; // set to true if value in sliderValues is different from respective value in prevSliderValues
    public bool active2100 = false;

    public GameObject loadingCircles; // will be activated whilst the environment doesn´t match the slider values yet

    // baseline values for each graph type are stored in order to restore the initial state when exiting the museum
    private String baselineTemperature;
    private String baselineCO2;
    private String baselineGreenhouse;
    private String baselineSeaLevel;
    private String baselineOceanAcidification;
    private String baselineAirPollution;

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

    // mouse click robots
    private MouseClickRobot graphRobot;
    private MouseClickRobot impactGraphRobot;
    private MouseClickRobot expandRobot;
    private MouseClickRobot extractRobot;

    // will store the start time of the readOutAndApplyValues() function
    private float ROAAstartTime;

    // Creates an array of all the keys for the slider related dictionaries
    private static string[] DictionaryKeys = new string[]
    {
        "Energy Supply : Coal",
        "Energy Supply : Renewables",
        "Energy Supply : Oil",
        "Energy Supply : Nuclear",
        "Energy Supply : Natural Gas",
        "Energy Supply : New Zero-Carbon",
        "Energy Supply : Bioenergy",
        "Energy Supply : Carbon Price",
        "Transport : Energy Efficiency",
        "Transport : Electrification",
        "Buildings and Industry : Energy Efficiency",
        "Buildings and Industry : Electrification",
        "Growth : Population",
        "Growth : Economic Growth",
        "Land and Industry Emissions : Deforestation",
        "Land and Industry Emissions : Methane &amp; Other",
        "Carbon Removal : Afforestation",
        "Carbon Removal : Technological"
    };

    // Enum of the graph types that are to be accessed
    public enum GraphTypes
    {
        TEMPERATURE,
        CO2,
        GREENHOUSE,
        SEA_LEVEL,
        OCEAN_ACIDIFICATION,
        AIR_POLLUTION
    }

    /* 
     * Start is called before the first frame update
     * Will initialize the dictionnaries
     */
    void Start()
    {
        this.sliderValues = new Dictionary<string, float>();
        this.prevSliderValues = new Dictionary<string, float>();

        // initialize values in sliderValues with -1 for every key in DictionaryKeys
        foreach (string key in BrowserSync.DictionaryKeys)
        {
            this.sliderValues.Add(key, -1f);
        }

        // initialize values in prevSliderValues with -1 for every key in DictionaryKeys
        foreach (string key in BrowserSync.DictionaryKeys)
        {
            this.prevSliderValues.Add(key, -1f);
        }

        this.sliderProxies = new Dictionary<string, MouseClickRobot>();

        // initialize keys for sliderProxies by convention [0-5]_key, [0-5] representing the 6 hidden browsers
        // then adds the respective mouseClickRobot to the dictionary
        foreach (string key in BrowserSync.DictionaryKeys)
        {
            int counter = 0;
            foreach (Browser browser in this.refBrowser)
            {
                MouseClickRobot robot = null;

                this.sliderProxies.Add(counter.ToString() + "_" + key, robot);
                //Debug.LogWarning("key: " + counter.ToString() + "_" + key);
                for (int i = 0; i < browser.transform.childCount; i++)
                {
                    if (browser.transform.GetChild(i).gameObject.GetComponent<MouseClickRobot>())
                    {
                        MouseClickRobot cRobot = browser.transform.GetChild(i).gameObject.GetComponent<MouseClickRobot>();

                        if (this.checkProxyTypeKeyMatch(cRobot.proxyType, key))
                        {
                            this.sliderProxies[counter.ToString() + "_" + key] = cRobot;
                        }
                    }
                }
                counter++;
            }


        }

        this.graphData = new Dictionary<BrowserSync.GraphTypes, string>();

        // initialize graphData with type and an empty string
        foreach (BrowserSync.GraphTypes type in Enum.GetValues(typeof(BrowserSync.GraphTypes)))
        {
            this.graphData.Add(type, "");
        }

        this.graphProxies = new Dictionary<GraphTypes, MouseClickRobot>();

        // adds the respective robots to graphProxies 
        // for some reason, they work on all the hidden browsers, which is not the case for the slider proxies
        // so for now there is no need to enlarge the dictionary with [0-5]_type
        // might be prone to errors but for now it works
        foreach (BrowserSync.GraphTypes type in Enum.GetValues(typeof(BrowserSync.GraphTypes)))
        {
            MouseClickRobot robot = null;
            this.graphProxies.Add(type, robot);

            for (int i = 0; i < this.refBrowser[0].transform.childCount; i++)
            {
                if (this.refBrowser[0].transform.GetChild(i).gameObject.GetComponent<MouseClickRobot>())
                {
                    MouseClickRobot cRobot = this.refBrowser[0].transform.GetChild(i).gameObject.GetComponent<MouseClickRobot>();
                    if (this.checkGraphProxyTypeMatch(cRobot.proxyType, type))
                    {
                        this.graphProxies[type] = cRobot;
                    }

                    if (cRobot.proxyType == MouseClickRobot.PROXY_TYPE.GRAPH)
                    {
                        this.graphRobot = cRobot;
                    }
                    else if (cRobot.proxyType == MouseClickRobot.PROXY_TYPE.IMPACT_GRAPH)
                    {
                        this.impactGraphRobot = cRobot;
                    }
                    else if (cRobot.proxyType == MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXPAND)
                    {
                        this.expandRobot = cRobot;
                    }
                    else if (cRobot.proxyType == MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXTRACT)
                    {
                        this.extractRobot = cRobot;
                    }
                }
            }
        }

        // error messages if any is missing
        if (!this.impactGraphRobot)
        {
            Debug.LogError("proxy with type " + MouseClickRobot.PROXY_TYPE.IMPACT_GRAPH + " is missing...");
        }
        if (!this.graphRobot)
        {
            Debug.LogError("proxy with type " + MouseClickRobot.PROXY_TYPE.GRAPH + " is missing...");
        }
        if (!this.expandRobot)
        {
            Debug.LogError("proxy with type " + MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXPAND + " is missing...");
        }
        if (!this.extractRobot)
        {
            Debug.LogError("proxy with type " + MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXTRACT + " is missing...");
        }
        set2022();
    }

    // Checks if correct graph type was clicked
    private bool checkGraphProxyTypeMatch(MouseClickRobot.PROXY_TYPE type, BrowserSync.GraphTypes graph)
    {
        switch (type)
        {
            case MouseClickRobot.PROXY_TYPE.TEMP_GRAPH:
                return graph == BrowserSync.GraphTypes.TEMPERATURE;
            case MouseClickRobot.PROXY_TYPE.CO2_GRAPH:
                return graph == BrowserSync.GraphTypes.CO2;
            case MouseClickRobot.PROXY_TYPE.GREENHOUSE_GRAPH:
                return graph == BrowserSync.GraphTypes.GREENHOUSE;
            case MouseClickRobot.PROXY_TYPE.SEA_LEVEL_GRAPH:
                return graph == BrowserSync.GraphTypes.SEA_LEVEL;
            case MouseClickRobot.PROXY_TYPE.OCEAN_ACIDIFICATION_GRAPH:
                return graph == BrowserSync.GraphTypes.OCEAN_ACIDIFICATION;
            case MouseClickRobot.PROXY_TYPE.AIR_POLLUTION_GRAPH:
                return graph == BrowserSync.GraphTypes.AIR_POLLUTION;
            // case MouseClickRobot.PROXY_TYPE.CROP_GRAPH:
            //     return graph == BrowserSync.GraphTypes.CROP;
            default:
                return false;
        }
    }

    // Checks if correct slider was accessed
    private bool checkProxyTypeKeyMatch(MouseClickRobot.PROXY_TYPE type, string key)
    {
        switch (type)
        {
            case MouseClickRobot.PROXY_TYPE.AFFORESTATION:
                return key == "Carbon Removal : Afforestation";
            case MouseClickRobot.PROXY_TYPE.BIOENERGY:
                return key == "Energy Supply : Bioenergy";
            case MouseClickRobot.PROXY_TYPE.BUILDINGS_ELECTRIFICATION:
                return key == "Buildings and Industry : Electrification";
            case MouseClickRobot.PROXY_TYPE.BUILDINGS_ENERGY_EFFICIENCY:
                return key == "Buildings and Industry : Energy Efficiency";
            case MouseClickRobot.PROXY_TYPE.CARBON_PRICE:
                return key == "Energy Supply : Carbon Price";
            case MouseClickRobot.PROXY_TYPE.COAL:
                return key == "Energy Supply : Coal";
            case MouseClickRobot.PROXY_TYPE.DEFORESTATION:
                return key == "Land and Industry Emissions : Deforestation";
            case MouseClickRobot.PROXY_TYPE.GROWTH_ECONOMY:
                return key == "Growth : Economic Growth";
            case MouseClickRobot.PROXY_TYPE.GROWTH_POPULATION:
                return key == "Growth : Population";
            case MouseClickRobot.PROXY_TYPE.METHANE:
                return key == "Land and Industry Emissions : Methane &amp; Other";
            case MouseClickRobot.PROXY_TYPE.NATURAL_GAS:
                return key == "Energy Supply : Natural Gas";
            case MouseClickRobot.PROXY_TYPE.NUCLEAR:
                return key == "Energy Supply : Nuclear";
            case MouseClickRobot.PROXY_TYPE.OIL:
                return key == "Energy Supply : Oil";
            case MouseClickRobot.PROXY_TYPE.RENEWABLES:
                return key == "Energy Supply : Renewables";
            case MouseClickRobot.PROXY_TYPE.TECHNOLOGICAL:
                return key == "Carbon Removal : Technological";
            case MouseClickRobot.PROXY_TYPE.TRANSPORT_ELECTRIFICATION:
                return key == "Transport : Electrification";
            case MouseClickRobot.PROXY_TYPE.TRANSPORT_ENERGY_EFFICIENCY:
                return key == "Transport : Energy Efficiency";
            case MouseClickRobot.PROXY_TYPE.ZERO_CARBON:
                return key == "Energy Supply : New Zero-Carbon";
            default:
                return false;
        }
    }

    // Update is called once per frame
    // checks if the user quits the museum (if yes, takes consequences below) or if normal update (checkBusy) should be conducted
    void Update()
    {
        if (!quit) // quitting takes some time, prevents doing anything whilst quitting
        {
            if (Input.GetKey("escape")) // press escape or press quit button (see script QuitMuseum) to quit the museum
            {
                StopAllCoroutines();
                this.quit = true;
                this.busy = false;
                //resetMaterials();
                //Debug.Log("Muesum has quit");
                Application.Quit();
            }
            else checkBusy(); // regular update: if not busy, slider values will be mapped to environment
        }

    }

    // if not busy: start mapping current slider values to the environment by calling readOutAndApplyValues()
    private void checkBusy()
    {
        if (!this.busy) // prevents executions whilst previous adaptions are still made
        {
            ROAAstartTime = Time.time; // for debugging purposes (to measure duration of readOutAndApplyValues())
            this.busy = true; // prevents parallel runs of the routine
            StartCoroutine(this.readOutAndApplyValues());
        }
    }

    // easiest way to copy states, but it is quite slow, since the browser refresh takes quite some time via the plugin
    // Doesn´t seem to be in use
    private void SyncURL()
    {
        /*        this.refBrowser.Url = this.browser.Url;
                this.refBrowser1.Url = this.browser.Url;
                this.refBrowser2.Url = this.browser.Url;
                this.refBrowser3.Url = this.browser.Url;
                this.refBrowser4.Url = this.browser.Url;
                this.refBrowser5.Url = this.browser.Url;
                this.refBrowser6.Url = this.browser.Url;
          */
    }

    // takes upper and lower limit of a range and a value and returns a float between 0 and 1 that indicates the position between these limits
    // will be used for mapping value of graph data to the color gradient / height
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

    // resets the material colors to the colors they have at baseline values
    
    /* private void resetMaterials()
    {
        var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.NumberDecimalSeparator = ".";

        // to keep the script adapdable to changes in the En-ROADS simulator, baseline values are always fetched from the browser
        // however, to keep it from crashing if the function is called before all baseline values are fetched, the current baseline values are stored here
        if (firstTime)
        {
            baselineTemperature = "3.605";
            baselineCO2 = "717.8";
            baselineGreenhouse = "907.9";
            baselineSeaLevel = "0.7119";
            baselineOceanAcidification = "7.83";
            baselineAirPollution = "43.42";
        }

        // In the following, the materials and positions will be resetted, ordered by graph that affects the respective objects

        // TEMPERATURE
        lower = 0.9f; // minimum - temperature increase
        upper = 5.8f; // maximum - temperature increase

        tree1.color = gradients.gradientTree1.Evaluate(valToPerc(lower, upper, float.Parse(baselineTemperature, ci)));
        tree2.color = gradients.gradientTree2.Evaluate(valToPerc(lower, upper, float.Parse(baselineTemperature, ci)));
        tree3.color = gradients.gradientTree3.Evaluate(valToPerc(lower, upper, float.Parse(baselineTemperature, ci)));
        ground.color = gradients.gradientGround.Evaluate(valToPerc(lower, upper, float.Parse(baselineTemperature, ci)));
        mountain.color = gradients.gradientMountain.Evaluate(valToPerc(lower, upper, float.Parse(baselineTemperature, ci)));

        posLake = objLake.transform.position;
        float lakeMax = -5.01f;
        float lakeMin = -8f;
        yLake = lakeMax - ((-1f) * valToPerc(lower, upper, float.Parse(baselineTemperature, ci)) * (lakeMin - lakeMax));
        posLake.y = yLake;
        objLake.transform.position = posLake;

        // CO2
        lower = 325.0f; // minimum - CO2 concentration
        upper = 1438.0f; // maximum - CO2 concentration

        ParticleSystem.MainModule cloudMain = psClouds.main;
        cloudMain.maxParticles = (int)(valToPerc(lower, upper, float.Parse(baselineAirPollution, ci)) * 140);
        clouds.color = gradients.gradientClouds.Evaluate(valToPerc(lower, upper, float.Parse(baselineCO2, ci)));

        // GREENHOUSE
        lower = 337.0f; // minimum - Greenhouse Gas concentration
        upper = 2310.0f; // maximum - Greenhouse Gas concentration

        RenderSettings.skybox.SetColor("_SkyTint", gradients.gradientSkybox.Evaluate(valToPerc(lower, upper, float.Parse(baselineGreenhouse, ci))));

        // SEA_LEVEL
        lower = 0.4458f; // minimum - sea level rise
        upper = 0.8628f; // maximum - sea level rise

        posOcean = objOcean.transform.position;

        float oceanMax = -2f;
        float oceanMin = -15.5f;
        yOcean = oceanMin + ((-1f) * valToPerc(lower, upper, float.Parse(baselineSeaLevel, ci)) * (oceanMin - oceanMax));
        posOcean.y = yOcean;
        objOcean.transform.position = posOcean;

        posIceberg = objIceberg.transform.position;

        float icebergMax = -15f;
        float icebergMin = -125f;
        yIceberg = icebergMax - ((-1f) * valToPerc(lower, upper, float.Parse(baselineGreenhouse, ci)) * (icebergMin - icebergMax));
        posIceberg.y = yIceberg;
        objIceberg.transform.position = posIceberg;

        // OCEAN_ACIDIFICATION
        lower = -8.12f; // optimal pH-value
        upper = -7.47f; // undesired pH-Value

        ocean.SetColor("_BaseColor", gradients.gradientOcean.Evaluate(valToPerc(lower, upper, float.Parse(baselineOceanAcidification, ci) * (-1f))));
        lake.color = gradients.gradientOcean.Evaluate(valToPerc(lower, upper, float.Parse(baselineOceanAcidification, ci) * (-1f)));

        // AIR_POLLUTION
        lower = 0.4f; // minimal air pollution
        upper = 310f; // maximal air pollution

        ParticleSystem.MainModule smogMain = psSmog.main;
        smogMain.maxParticles = (int)(valToPerc(lower, upper, float.Parse(baselineAirPollution, ci)) * 140);
        smog.color = gradients.gradientSmog.Evaluate(valToPerc(lower, upper, float.Parse(baselineCO2, ci)));
    }
    */

    // reads out the slider values, applies them to the hidden SyncBrowser, copies the predictions and applies them to the environment
    private IEnumerator readOutAndApplyValues()
    {
        // we fetch the whole document as html and work with the text in Unity, this is faster than subsequent js queries
        var promise = this.browser.EvalJS("document.body.innerHTML;");
        yield return promise.ToWaitFor();
        //Debug.LogWarning("Promise ready.");

        string pageData = promise.Value.Value.ToString();
        //Debug.LogWarning(pageData);

        // the sliders are grouped in section like energy supply etc.
        string[] sections = pageData.Split(new string[] { "section-container" }, StringSplitOptions.RemoveEmptyEntries);

        // if not all of them have been fetched, stop (can happen due to loading times)
        if (sections.Length < 2)
        {
            busy = false;
            yield break;
        }

        for (int i = 0; i < sections.Length; i++)
        {
            if (sections[i].Contains("div class=\"section-title"))
            {
                // the section title is at the second entity i.e. at the third position behind an opening >
                string title = sections[i].Split('>')[2].Split('<')[0];
                // we go through the sliders
                string[] tokens = sections[i].Split(new string[] { "slider-container" }, StringSplitOptions.RemoveEmptyEntries);
                // skip the first element (section container stuff)
                for (int j = 1; j < tokens.Length; j++)
                {
                    // the title is in the first span element
                    string sliderTitle = tokens[j].Split(new string[] { "</span>" },
                        StringSplitOptions.RemoveEmptyEntries)[0].Split(new string[] { ">" },
                        StringSplitOptions.RemoveEmptyEntries)[3];
                    //Debug.Log(title + " : " + sliderTitle);
                    // working with the input ID would be much more elegant, but given that we fake UI interactions, the percentage is more useful
                    // an ugly mess, the question is how long this remains valid, if the website changes, this stuff here has to be changed as well

                    // we take the first slider (the second one seems just to be there for layout reasons)
                    string sliderValue = tokens[j].Split(new string[] { "class=\"slider-handle min-slider-handle round" },
                        StringSplitOptions.RemoveEmptyEntries)[1];
                    // go to the horizontal percentage...
                    sliderValue = sliderValue.Split(new string[] { "left: " },
                        StringSplitOptions.RemoveEmptyEntries)[1];
                    // and obtain the according value
                    sliderValue = sliderValue.Split(new string[] { "%;\">" },
                        StringSplitOptions.RemoveEmptyEntries)[0];
                    //Debug.Log(title + " : " + sliderTitle + " : " + sliderValue);
                    
                    if (title.Equals("Energieversorgung"))
                    {
                        title = "Energy Supply";
                    } else if (title.Equals("Transport &amp; Verkehr"))
                    {
                        title = "Transport";
                    } else if (title.Equals("Gebäude und Industrie"))
                    {
                        title = "Buildings and Industry";
                    }
                    else if (title.Equals("Wachstum"))
                    {
                        title = "Growth";
                    }
                    else if (title.Equals("Landnutzungs- und Industrieemissionen"))
                    {
                        title = "Land and Industry Emissions";
                    }
                    else if (title.Equals("CO2-Abbau"))
                    {
                        title = "Carbon Removal";
                    }

                    if (sliderTitle.Equals("Kohle"))
                    {
                        sliderTitle = "Coal";
                    } else if (sliderTitle.Equals("Erneuerbare Energien"))
                    {
                        sliderTitle = "Renewables";
                    }
                    else if (sliderTitle.Equals("Öl"))
                    {
                        sliderTitle = "Oil";
                    }
                    else if (sliderTitle.Equals("Kernkraft"))
                    {
                        sliderTitle = "Nuclear";
                    }
                    else if (sliderTitle.Equals("Erdgas"))
                    {
                        sliderTitle = "Natural Gas";
                    }
                    else if (sliderTitle.Equals("New Zero-Carbon"))
                    {
                        sliderTitle = "New Zero-Carbon";
                    }
                    else if (sliderTitle.Equals("Bioenergie"))
                    {
                        sliderTitle = "Bioenergy";
                    }
                    else if (sliderTitle.Equals("CO2-Preis"))
                    {
                        sliderTitle = "Carbon Price";
                    }
                    else if (sliderTitle.Equals("Energieeffizienz"))
                    {
                        sliderTitle = "Energy Efficiency";
                    }
                    else if (sliderTitle.Equals("Elektrifizierung"))
                    {
                        sliderTitle = "Electrification";
                    }
                    else if (sliderTitle.Equals("Energieeffizienz"))
                    {
                        sliderTitle = "Energy Efficiency";
                    }
                    else if (sliderTitle.Equals("Elektrifizierung"))
                    {
                        sliderTitle = "Electrification";
                    }
                    else if (sliderTitle.Equals("Bevölkerungswachstum"))
                    {
                        sliderTitle = "Population";
                    }
                    else if (sliderTitle.Equals("Wirtschaftswachstum"))
                    {
                        sliderTitle = "Economic Growth";
                    }
                    else if (sliderTitle.Equals("Entwaldung"))
                    {
                        sliderTitle = "Deforestation";
                    }
                    else if (sliderTitle.Equals("Methan &amp; andere"))
                    {
                        sliderTitle = "Methane &amp; Other";
                    }
                    else if (sliderTitle.Equals("Aufforstung"))
                    {
                        sliderTitle = "Afforestation";
                    }
                    else if (sliderTitle.Equals("Technologisch"))
                    {
                        sliderTitle = "Technological";
                    }


                    // update the dictionary
                    this.sliderValues[title + " : " + sliderTitle] = float.Parse(sliderValue, CultureInfo.InvariantCulture);
                    //Debug.LogWarning(this.sliderValues[title + " : " + sliderTitle]);
                }
            }
        }

        float sliderValuesStart = Time.realtimeSinceStartup; // time stamp to measure the time that applying the slider values to the hidden browsers takes
        hasChanged = false;

        // Go through all sliders
        foreach (string key in this.sliderValues.Keys)
        {
            MouseClickRobot robot = null;
            float value = -1f;
            float prevValue = -1f;

            this.sliderValues.TryGetValue(key, out value); // makes "value" the slider value from dictionary sliderValues
            this.prevSliderValues.TryGetValue(key, out prevValue); // makes "prevValue" the slider value from dictionary prevSliderValues
            //Debug.LogWarning("Value1: " + value + " PrevValue: " + prevValue);
            if (value >= 0f && value != prevValue) // if proper value is accessed and value is different to the previous one..
            {
                hasChanged = true;
                loadingCircles.SetActive(true); // makes loading circle and message "Adapting Environment" visible
                prevSliderValues[key] = value; // update prevSliderValues for the next run
                //Debug.LogWarning("New prev for " + key + " is: " + value);
                value = value * 0.01f;
                int counter = 0;
                foreach (PointerUIBase refBrowserUI in pointerUIBases) // apply change to all hidden browsers
                {
                    bool test = this.sliderProxies.TryGetValue(counter.ToString() + "_" + key, out robot);
                    //Debug.LogWarning(key + ": " + test);

                    if (robot)
                    {
                        robot.setPercentage(value);
                        //Debug.LogWarning("Value2: " + value + ", Proxytype: " + robot.proxyType + ", robot: " + robot);

                        refBrowserUI.SetRelevantProxyType(robot.proxyType);

                        while (refBrowserUI.relevantProxyType != MouseClickRobot.PROXY_TYPE.NONE)
                        {
                            yield return 2;
                        }
                    }

                    //Debug.LogWarning(robot.proxyType + " done");
                    counter++;
                }

            }
        }
        float sliderValuesDone = Time.realtimeSinceStartup;
        float durationSliderValues = sliderValuesDone - sliderValuesStart;
        //Debug.LogWarning("Duration for six browser instances is: " + durationSliderValues + "s."); // 1 instance: 1.5-2 sec, 6 instances 1.3-2.5sec

        // if readOutAndApplyValues() is called for the first time, the proper graphs have to be opened
        if (firstTime)
        {
            int counter = 0; // ranges from 0-5, responsible for the instance of the 6 hidden browsers
            float graphStart = Time.realtimeSinceStartup; // measures duration of the graph opening
            this.firstTime = false;

            // switch through graphs
            foreach (BrowserSync.GraphTypes type in this.graphProxies.Keys)
            {
                MouseClickRobot robot = null;
                this.graphProxies.TryGetValue(type, out robot); // set "robot" to respective mouseClickRobot of graphProxies

                if (robot)
                {
                    List<MouseClickRobot.PROXY_TYPE> states = new List<MouseClickRobot.PROXY_TYPE>(); // list works like a to-do list of raycasts

                    states.Add(this.graphRobot.proxyType); // adds the robot that opens the graph menu to the to-do list
                    states.Add(this.impactGraphRobot.proxyType); // adds the robot that chooses the impacts option in the graph menu to the to-do list
                    states.Add(robot.proxyType); // adds the robot that chooses the respective graph type in the impacts menu to the to-do list

                    float startTime = Time.realtimeSinceStartup;
                    while (states.Count > 0) // while to-do list is not empty...
                    {
                        pointerUIBases[counter].SetRelevantProxyType(states[0]); // counter choosing the hidden browser 

                        while (this.pointerUIBases[counter].relevantProxyType != MouseClickRobot.PROXY_TYPE.NONE && Time.realtimeSinceStartup - startTime < 10.0f)
                        {
                            yield return 2;
                        }

                        states.RemoveAt(0); // remove fist item in to-do list -> it is done

                    }

                    yield return 10;

                    while (this.pointerUIBases[counter].relevantProxyType != MouseClickRobot.PROXY_TYPE.NONE && Time.realtimeSinceStartup - startTime < 10.0f)
                    {
                        yield return 2;
                    }

                    while (this.pointerUIBases[counter].relevantProxyType != MouseClickRobot.PROXY_TYPE.NONE && Time.realtimeSinceStartup - startTime < 10.0f)
                    {
                        yield return 2;
                    }

                    if (Time.realtimeSinceStartup - startTime < 10.0f)
                    {
                        Debug.LogWarning("Timeout");
                    }

                    counter++;
                }
            }
            yield return new WaitForSeconds(0.5f);
            float graphDone = Time.realtimeSinceStartup;
            float durationGraphOpening = graphDone - graphStart;
            //Debug.LogWarning("Duration graph opening is: " + durationGraphOpening + "s.");
        }

        if (hasChanged) // in case any of the sliders were moved to a new position, the environment has to display the changes
        {
            // expand the image (graph) options
            foreach (PointerUIBase refBrowserUI in pointerUIBases)
            {
                refBrowserUI.SetRelevantProxyType(MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXPAND);
            }

            // the context menu might take some time to load; this delay is really long, but it seems necessary
            yield return new WaitForSeconds(0.7f);

            // extract the graph information
            int counter = 0; // keeps track of which graphType is relevant in each run
            string tempData;
            string controlTemp = "Temperature Change (Degrees Celsius)";
            string controlCO2 = "CO2 Concentration (CO2 parts per million (ppm))";
            string controlGreen = "Greenhouse Gas Concentration (CO2 equivalent parts per million (ppm))";
            string controlSea = "Sea Level Rise (Meters (m))";
            string controlAcid = "Ocean Acidification (pH)";
            string controlAir = "Air Pollution from Energy — PM2.5 Emissions (Megatons PM2.5/year)";
            string controlTempGerman = "Temperaturänderung (Grad Celsius)";
            string controlCO2German = "CO2-Konzentration (CO2, parts per million (ppm))";
            string controlGreenGerman = "Treibhausgas-Konzentration (CO2-Äquivalent, parts per million (ppm))";
            string controlSeaGerman = "Anstieg des Meeresspiegels (Meter (m))";
            string controlAcidGerman = "Versauerung der Ozeane (pH)";
            string controlAirGerman = "Luftverschmutzung aus der Energieerzeugung – PM2,5-Emissionen (Megatonnen PM2,5/Jahr)";
            
            foreach (PointerUIBase refBrowserUI in pointerUIBases)
            {
                refBrowserUI.SetRelevantProxyType(MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXTRACT); // use the extract robot to save data to copyBuffer
                yield return new WaitForSeconds(0.3f);
                this.graphData[(GraphTypes)counter] = GUIUtility.systemCopyBuffer; // save data from copyBuffer to graphData dictionnary

                // as the time that expanding and extracting takes can vary extremely,
                // it is necessary to run a check if the data saved for the respective graph type is correct
                tempData = this.graphData[(GraphTypes)counter]; // stores extracted data in tempData
                //Debug.LogWarning("Tempdata:" + tempData);
                var typeOfDataArray = tempData.Split('\n'); // split at new lines
                tempData = typeOfDataArray[0]; // get first line
                tempData = tempData.Remove(tempData.Length - 1); // remove last element, otherwise they don´t match
                int whileCounter = 0; // counts how often the process has been repeated
                // while the first line of the extracted data doesn´t match the expected data type, repeat extraction process
                while (!(counter == 0 && (tempData.Equals(controlTemp)  || tempData.Equals(controlTempGerman)  ) ||
                         counter == 1 && (tempData.Equals(controlCO2)   || tempData.Equals(controlCO2German)   ) ||
                         counter == 2 && (tempData.Equals(controlGreen) || tempData.Equals(controlGreenGerman) ) ||
                         counter == 3 && (tempData.Equals(controlSea)   || tempData.Equals(controlSeaGerman)   ) ||
                         counter == 4 && (tempData.Equals(controlAcid)  || tempData.Equals(controlAcidGerman)  ) ||
                         counter == 5 && (tempData.Equals(controlAir)   || tempData.Equals(controlAirGerman)   )))
                {
                    if (whileCounter > 2) // major error seems to have happened -> stop trying and restart whole process
                    {
                        busy = false;
                        yield break;
                    }
                    //Debug.LogWarning("Graph type is wrong. Counter is: " + counter + ", tempData is: " + tempData);

                    // In case the dropdown menu was still expanded, the expand robot would close it,
                    // therefore it´s better to first use the extract robot to be safe and then run the extraction process anew
                    refBrowserUI.SetRelevantProxyType(MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXTRACT);
                    // repeated extracting proccess with longer waiting times
                    refBrowserUI.SetRelevantProxyType(MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXPAND);
                    yield return new WaitForSeconds(0.7f);
                    refBrowserUI.SetRelevantProxyType(MouseClickRobot.PROXY_TYPE.CENTER_IMAGE_EXTRACT);
                    yield return new WaitForSeconds(0.5f);
                    this.graphData[(GraphTypes)counter] = GUIUtility.systemCopyBuffer;
                    tempData = this.graphData[(GraphTypes)counter]; // stores extracted data in tempData
                    typeOfDataArray = tempData.Split('\n'); // split at new lines
                    tempData = typeOfDataArray[0]; //get fist line
                    tempData = tempData.Remove(tempData.Length - 1); // remove last element, otherwise they don´t match
                    whileCounter++;
                }

                // Debug.LogWarning("Successful comparison.");
                counter++;
            }

            // filters data for relevant values and applies them
            apply();
            loadingCircles.SetActive(false); // deactivates loading circle
        }

        this.busy = false;
        float ROAAduration = Time.time - ROAAstartTime;
        //Debug.Log("Duration of readOutAndApply: " + ROAAduration);

        yield return null;
    }

    public void apply()
    {
        foreach (BrowserSync.GraphTypes type in this.graphData.Keys)
        {
            // prevents confusion with the separators
            var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.NumberDecimalSeparator = ".";
            string data = this.graphData[type]; // extracts currently relevant data from graphData dictionary
                                                //Debug.LogWarning("DATA of " + type + ": " + data);
            var splittedData = data.Split('\n'); // split at new lines
            var data2100 = splittedData[splittedData.Length - 2]; // save last line (contains relevant data of year 2100)
            var splittedData2100 = data2100.Split('\t'); // split at tab
            var baseline = splittedData2100[splittedData2100.Length - 2]; // save second last value (baseline value)
            var prognosis = splittedData2100[splittedData2100.Length - 1]; // save last value (current prognosis)
                                                                           //Debug.LogWarning(type + "Baseline: " + baseline + ", prognosis: " + prognosis);


            // -------------- Applies the prognosis to the environment -------------- 
            if (active2100)
            {
                // TEMPERATURE affects tree color, ground color, mountain color and lake height
                if (type == GraphTypes.TEMPERATURE)
                {
                    baselineTemperature = baseline; // saves baseline values for resetting
                                                    // meaning of lower and upper for all the following cases already explained in resetMaterials()
                    lower = 0.9f;
                    upper = 5.8f;

                    tree1.color = gradients.gradientTree1.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    // make leaves disappear if prognosis >= 85% of worst case value:
                    if (valToPerc(lower, upper, float.Parse(prognosis, ci)) >= 0.85)
                    {
                        tree1.color = new Color(tree1.color.r, tree1.color.g, tree1.color.b, 0f);
                    }
                    tree2.color = gradients.gradientTree2.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    // make leaves disappear if prognosis >= 80% of worst case value:
                    if (valToPerc(lower, upper, float.Parse(prognosis, ci)) >= 0.80)
                    {
                        tree2.color = new Color(tree2.color.r, tree2.color.g, tree2.color.b, 0f);
                    }
                    tree3.color = gradients.gradientTree3.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    // make leaves disappear if prognosis >= 70% of worst case value:
                    if (valToPerc(lower, upper, float.Parse(prognosis, ci)) >= 0.70)
                    {
                        tree3.color = new Color(tree3.color.r, tree3.color.g, tree3.color.b, 0f);
                    }

                    ground.color = gradients.gradientGround.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    mountain.color = gradients.gradientMountain.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    posLake = objLake.transform.position;

                    float lakeMax = -5.01f;
                    float lakeMin = -8f;

                    yLake = lakeMax - ((-1f) * valToPerc(lower, upper, float.Parse(prognosis, ci)) * (lakeMin - lakeMax));
                    posLake.y = yLake;
                    objLake.transform.position = posLake;

                    //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }
                // CO2-CONCENTRATION affects cloud color and concentration
                else if (type == GraphTypes.CO2)
                {
                    baselineCO2 = baseline;
                    lower = 325.0f;
                    upper = 1438.0f;

                    ParticleSystem.MainModule cloudsMain = psClouds.main;
                    cloudsMain.maxParticles = (int)(valToPerc(lower, upper, float.Parse(prognosis, ci)) * 140);
                    clouds.color = gradients.gradientClouds.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }
                // GREENHOUSE-GASSES affects skybox color
                else if (type == GraphTypes.GREENHOUSE)
                {
                    baselineGreenhouse = baseline;
                    lower = 337.0f;
                    upper = 2310.0f;

                    RenderSettings.skybox.SetColor("_SkyTint", gradients.gradientSkybox.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci))));

                    //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }
                // SEA LEVEL affects sea level and iceberg height
                else if (type == GraphTypes.SEA_LEVEL)
                {
                    baselineSeaLevel = baseline;
                    lower = 0.4458f;
                    upper = 0.8628f;

                    posOcean = objOcean.transform.position;

                    float oceanMax = -2f;
                    float oceanMin = -15.5f;
                    yOcean = oceanMin + ((-1f) * valToPerc(lower, upper, float.Parse(prognosis, ci)) * (oceanMin - oceanMax));
                    posOcean.y = yOcean;
                    objOcean.transform.position = posOcean;

                    posIceberg = objIceberg.transform.position;

                    float icebergMax = -15f;
                    float icebergMin = -125f;
                    yIceberg = icebergMax - ((-1f) * valToPerc(lower, upper, float.Parse(prognosis, ci)) * (icebergMin - icebergMax));
                    posIceberg.y = yIceberg;
                    objIceberg.transform.position = posIceberg;

                    Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }
                // OCEAN ACIDIFICATION affects ocean color
                else if (type == GraphTypes.OCEAN_ACIDIFICATION)
                {
                    baselineOceanAcidification = baseline;
                    lower = -8.12f;
                    upper = -7.47f;

                    ocean.SetColor("_BaseColor", gradients.gradientOcean.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci) * (-1f))));

                    //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }
                // AIR POLLUTION affects smog
                else if (type == GraphTypes.AIR_POLLUTION)
                {
                    baselineAirPollution = baseline;
                    lower = 0.4f;
                    upper = 310f;

                    ParticleSystem.MainModule smogMain = psSmog.main;
                    smogMain.maxParticles = (int)(valToPerc(lower, upper, float.Parse(prognosis, ci)) * 140);
                    smog.color = gradients.gradientSmog.Evaluate(valToPerc(lower, upper, float.Parse(prognosis, ci)));
                    //Debug.Log(type + ": Baseline value is " + float.Parse(baseline, ci) + ", prognosis for year 2100 is " + float.Parse(prognosis, ci));
                }


            }
        }
    }


    // -------------------- Public Getter and Setter -------------------- 

    // sets bool quit to true (for script QuitMuseum)
    public void setQuit()
    {
        this.quit = true;
    }
    // sets bool busy to false (for script QuitMuseum)
    public void setNotBusy()
    {
        this.busy = false;
    }
    // return bool busy (for script QuitMuseum)
    public bool getBusy()
    {
        return this.busy;
    }
    // calls resetMaterials() (for script QuitMuseum)
    /*public void doResetMaterials()
    {
        resetMaterials();
    }
    */

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
