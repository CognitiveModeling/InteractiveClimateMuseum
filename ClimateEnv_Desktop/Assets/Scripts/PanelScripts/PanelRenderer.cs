using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script handles the rendering of the panels.
// It itself is not assigned in the editor.

public class PanelRenderer : MonoBehaviour
{
    // variables for the start screen
    public GameObject StartScreenContent;
    public GameObject StartScreenHeader;

    // variables for the general panel tab "General Information"
    public GameObject generalInfoContainer;
    public GameObject generalBackground;
    public GameObject generalHeader;

    // variables for the general panel tab "Correlations"
    public GameObject correlationChildPrefab;
    public GameObject correlationContainer;
    public GameObject[] corrImages;
    public GameObject corrHeaderBlack;
    public GameObject corrHeaderWhite;

    // variables for the parent, helping to get the right header and background
    public GameObject backgroundImageParent;
    public GameObject headerParent;

    // text file with panel text
    public TextAsset PanelText;
    private TMPro.TextMeshProUGUI contentText;

    // an instance of the helper script OnEnableScript for en-/disabling the header background
    private OnEnableScript contentScript;

    // child of the panel
    private string childName;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate Start Screen
        contentScript = this.StartScreenContent.gameObject.GetComponentInChildren<OnEnableScript>(true);

        // instantiate image and header
        // (class of contentScript (OnEnableScript) with specific image and header is assigned to start screen and every panel tab in the editor)
        contentScript.image = GameObject.Instantiate(generalBackground, backgroundImageParent.transform);
        contentScript.image.transform.localPosition = Vector3.zero;
        contentScript.image.GetComponent<RectTransform>();

        contentScript.header = GameObject.Instantiate(StartScreenHeader, headerParent.transform);
        contentScript.header.GetComponent<RectTransform>();

        // if the general tab "Correlations" is not active
        if (correlationContainer.activeSelf == false)
        {
            // instantiate all child tabs of General Information (Information, Examples, Key Dynamics, ...)
            for (int i = 0; i < this.generalInfoContainer.transform.childCount; i++)
            {
                contentScript = this.generalInfoContainer.transform.GetChild(i).gameObject.GetComponentInChildren<OnEnableScript>(true);

                // instantiate background image
                contentScript.image = GameObject.Instantiate(generalBackground, backgroundImageParent.transform);
                contentScript.image.transform.localPosition = Vector3.zero;
                contentScript.image.GetComponent<RectTransform>();

                // instantiate header
                contentScript.header = GameObject.Instantiate(generalHeader, headerParent.transform);
                contentScript.header.GetComponent<RectTransform>();

                // get content text from text asset (s. GetTextFromFile(string) below)
                childName = this.generalInfoContainer.transform.GetChild(i).gameObject.name;

                contentText = this.generalInfoContainer.transform.GetChild(i).transform.Find("Content").Find("Text (TMP)").gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
                contentText.text = GetTextFromFile(childName);

                // if there is no text available, deactivate the child tab
                if (contentText.text == "")
                {
                    this.generalInfoContainer.transform.GetChild(i).gameObject.SetActive(false);
                }

                // activate the container of General Info (for script CorrelationOnEnable())
                generalInfoContainer.SetActive(true);
            }
        }

        // if images of correlations exist, instantiate child tabs of "Correlations" (differ from panel to panel)
        if (corrImages.Length != 0)
        {
            // for each image (each child tab, respectively):
            for (int i = 0; i < corrImages.Length; i++)
            {
                // instantiate child tabs of "Correlations" (differ from panel to panel)
                GameObject correlationChild = GameObject.Instantiate(correlationChildPrefab, correlationContainer.transform);
                correlationChild.transform.name = corrImages[i].name;
                correlationChild.GetComponent<Toggle>().group = correlationContainer.GetComponent<ToggleGroup>();
                
                // if it is the first image/child, set its toggle to true (so its content is shown in the panel first after clicking on "Correlations")
                if (i == 0)
                {
                    correlationChild.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    correlationChild.GetComponent<Toggle>().isOn = false;
                }
                
                // write name of child into its label (tab heading)
                correlationChild.transform.Find("Label").GetComponent<Text>().text = corrImages[i].name;
                
                // define size of the child tab's toggle (s. SizeOfToggle() below)
                correlationChild.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeOfToggle(correlationChild.name), 30);

                // write content from text file (TMPro.TextMeshProUGUI) into Content part of child tab (s. GetTextFromFile(string) below)
                contentText = correlationChild.transform.transform.Find("Content").Find("Text (TMP)").gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                contentText.text = GetTextFromFile("{\"" + correlationChild.transform.name);
            }
        }

        // activate the container of Correlations (for script CorrelationOnEnable())
        correlationContainer.SetActive(true);

        // instantiate Correlation Background & Header for each child tab
        for (int i = 0; i < this.correlationContainer.transform.childCount; i++)
        {
            // save child tab's name
            string name = this.correlationContainer.transform.GetChild(i).name;

            // define content script with OnEnableScript of the child tab
            contentScript = this.correlationContainer.transform.GetChild(i).gameObject.GetComponentInChildren<OnEnableScript>(true);

            // instantiate header background dependent on the given panel name (s. InstantiateHeaderBackground(image, header) below)
            switch (name)
            {
                case "Carbon Removal":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Carbon Removal"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;

                case "TechnologicalCarbonRemoval":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("TechnologicalCarbonRemoval"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;


                case "Afforestation":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Afforestation"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;


                case "Emissions":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Emissions"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;


                case "Deforestation":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Deforestation"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;


                case "Methane & Co":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Methane & Co"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;


                case "Energy Supply":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Energy Supply"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;

                case "Carbon Pricing":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Carbon Pricing"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "New Technologies":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("New Technologies"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Nuclear":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Nuclear"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Renewables":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Renewables"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;


                case "Bioenergy":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Bioenergy"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;

                case "Natural Gas":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Natural Gas"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Oil":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Oil"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Coal":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Coal"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Transport":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Transport"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "T Electrification":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("T Electrification"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "T Efficiency":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("T Efficiency"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Buildings & Industry":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Buildings & Industry"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "BI Electrification":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("BI Electrification"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "BI Efficiency":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("BI Efficiency"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderBlack);
                            }
                        }
                    }
                    break;

                case "Growth":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Growth"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Economic":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Economic"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                case "Population":
                    if (contentScript)
                    {
                        foreach (GameObject img in corrImages)
                        {
                            if (img.name.StartsWith("Population"))
                            {
                                InstantiateHeaderBackground(img, corrHeaderWhite);
                            }
                        }
                    }
                    break;

                default:
                    break;

            }
        }
    }

    // instantiates the background of a header with an image
    void InstantiateHeaderBackground(GameObject img, GameObject header)
    {
        // instantiates image assigned to contentScript
        contentScript.image = GameObject.Instantiate(img, backgroundImageParent.transform);
        contentScript.image.transform.localPosition = Vector3.zero;
        contentScript.image.GetComponent<RectTransform>();

        // disables image
        contentScript.image.SetActive(false);

        // instantiates header assigned to contentScript
        contentScript.header = GameObject.Instantiate(header, headerParent.transform);
        contentScript.header.GetComponent<RectTransform>();

        // disables header
        contentScript.header.SetActive(false);
    }

    // returns the panel text depending on the given panel name
    string GetTextFromFile(string name)
    {
        // save panel text in new string
        string panelTextString = PanelText.ToString();
        // navigate to corresponding substring
        panelTextString = panelTextString.Substring(panelTextString.IndexOf(name.ToLower()) + 1);
        // remove name of child and ":"
        panelTextString = panelTextString.Substring(name.Length + 2, panelTextString.Length - (name.Length + 2));
        // remove substring behind corresponding substring
        panelTextString = panelTextString.Remove(panelTextString.IndexOf('"'));
        // make new line and bulletpoints readable
        panelTextString = panelTextString.Replace("\\n", "\n").Replace("\\u2022<indent=1em>", "\u2022<indent=1em>");

        return panelTextString;
    }

    // returns a specific size of a toggle depending on the given panel name
    int SizeOfToggle(string name)
    {
        switch (name)
        {
            case "Carbon Removal":
                return 140;

            case "Afforestation":
                return 110;

            case "Technological":
                return 110;


            case "Emissions":
                return 90;

            case "Deforestation":
                return 110;

            case "Methane & Co":
                return 120;


            case "Energy Supply":
                return 120;

            case "Coal":
                return 60;

            case "Renewables":
                return 110;

            case "Oil":
                return 50;

            case "Natural Gas":
                return 105;

            case "Bioenergy":
                return 90;

            case "Carbon Pricing":
                return 110;

            case "New Technology":
                return 110;

            case "Nuclear":
                return 70;


            case "Transport":
                return 90;

            case "T Efficiency":
                return 100;

            case "T Electrification":
                return 130;


            case "Buildings & Industry":
                return 160;

            case "BI Efficiency":
                return 110;

            case "BI Electrification":
                return 130;


            case "Growth":
                return 65;

            case "G Economic":
                return 95;

            case "G Population":
                return 110;


            default:
                return 200;
        }
    }
}
