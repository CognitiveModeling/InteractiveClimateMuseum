using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRenderer : MonoBehaviour
{
    public GameObject StartScreenContent;
    public GameObject startScreenHeader;

    public GameObject generalInfoContainer;

    public GameObject generalBackground;

    public GameObject generalHeader;

    public GameObject correlationChildPrefab;

    public GameObject correlationContainer;

    public GameObject[] corrImages;

    public GameObject corrHeaderBlack;

    public GameObject corrHeaderWhite;

    public GameObject backgroundImageParent;

    public GameObject headerParent;

    public TextAsset PanelText;

    private TMPro.TextMeshProUGUI contentText;

    private OnEnableScript contentScript;

    private string childName;
 
    // private float currentToggleOffset = 0f;
     

    // Start is called before the first frame update
    void Start()
    {
            // Instantiate Start Screen    
            contentScript = this.StartScreenContent.gameObject.GetComponentInChildren<OnEnableScript>(true);

            contentScript.image = GameObject.Instantiate(generalBackground, backgroundImageParent.transform);
            contentScript.image.transform.localPosition = Vector3.zero;
            contentScript.image.GetComponent<RectTransform>();

            contentScript.header = GameObject.Instantiate(startScreenHeader, headerParent.transform);
            contentScript.header.GetComponent<RectTransform>();


        if (correlationContainer.activeSelf == false)
        {
            // Instantiate General Informations
            for (int i = 0; i < this.generalInfoContainer.transform.childCount; i++)
            {
                contentScript = this.generalInfoContainer.transform.GetChild(i).gameObject.GetComponentInChildren<OnEnableScript>(true);

                // Background image
                contentScript.image = GameObject.Instantiate(generalBackground, backgroundImageParent.transform);
                contentScript.image.transform.localPosition = Vector3.zero;
                contentScript.image.GetComponent<RectTransform>();

                // Header
                contentScript.header = GameObject.Instantiate(generalHeader, headerParent.transform);
                contentScript.header.GetComponent<RectTransform>();

                // Get content text from text asset
                childName = this.generalInfoContainer.transform.GetChild(i).gameObject.name;

                contentText = this.generalInfoContainer.transform.GetChild(i).transform.Find("Content").Find("Text (TMP)").gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
                contentText.text = GetTextFromFile(childName);

                if (contentText.text == "")
                {
                    this.generalInfoContainer.transform.GetChild(i).gameObject.SetActive(false);
                }
 
                generalInfoContainer.SetActive(true); // For CorrectContentPosition()

            }
        }
        
        // Create Correlation Objects
        if (corrImages.Length != 0)
        {
            for (int i = 0; i < corrImages.Length; i++)
            {
                GameObject correlationChild = GameObject.Instantiate(correlationChildPrefab, correlationContainer.transform);
                correlationChild.transform.name = corrImages[i].name;
                correlationChild.GetComponent<Toggle>().group = correlationContainer.GetComponent<ToggleGroup>();
                if (i == 0)
                {
                    correlationChild.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    correlationChild.GetComponent<Toggle>().isOn = false;
                }
                correlationChild.transform.Find("Label").GetComponent<Text>().text = corrImages[i].name;

                correlationChild.GetComponent<RectTransform>().sizeDelta = new Vector2(SizeOfToggle(correlationChild.name), 30);
                
                contentText = correlationChild.transform.transform.Find("Content").Find("Text (TMP)").gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                contentText.text = GetTextFromFile("{\"" + correlationChild.transform.name);
            }
        }

        correlationContainer.SetActive(true); // For CorrectContentPosition()

        // Instantiate Correlation Background & Header
        for (int i = 0; i < this.correlationContainer.transform.childCount; i++)
        {
            string name = this.correlationContainer.transform.GetChild(i).name;

            contentScript = this.correlationContainer.transform.GetChild(i).gameObject.GetComponentInChildren<OnEnableScript>(true);

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

    void InstantiateHeaderBackground(GameObject img, GameObject header)
    {
        contentScript.image = GameObject.Instantiate(img,backgroundImageParent.transform);
        contentScript.image.transform.localPosition = Vector3.zero;
        contentScript.image.GetComponent<RectTransform>();
                                    
        contentScript.image.SetActive(false);

        contentScript.header = GameObject.Instantiate(header, headerParent.transform);
        contentScript.header.GetComponent<RectTransform>();

        contentScript.header.SetActive(false);
    }

    string GetTextFromFile(string name)
    {
            string panelTextString;
            panelTextString = PanelText.ToString();
            panelTextString = panelTextString.Substring(panelTextString.IndexOf(name.ToLower()) + 1); //Navigate to corresponding substring
            panelTextString = panelTextString.Substring(name.Length + 2, panelTextString.Length-(name.Length + 2)); //Remove name of child and ":"                
            panelTextString = panelTextString.Remove(panelTextString.IndexOf('"')); //Remove substring behind corresponding substring
            panelTextString = panelTextString.Replace("\\n", "\n").Replace("\\u2022<indent=1em>", "\u2022<indent=1em>"); //Make newline and bulletpoints readable

            return panelTextString;

    }

    int SizeOfToggle(string name)
    {
        switch(name)
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
