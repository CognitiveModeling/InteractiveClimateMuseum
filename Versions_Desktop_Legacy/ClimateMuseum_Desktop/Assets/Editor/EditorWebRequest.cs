using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;
using System;


public class EditorWebRequest : MonoBehaviour
{
    public static ObjectForJson objectForJson;

    static UnityWebRequest[] Requests;

    static List<string> Identifiers = new List<string>(); 
    static List<string> Topics = new List<string>();
    static List<string> TopicNames = new List<string>();
    static List<string> Urls = new List<string>();
    static List<string> Htmls = new List<string>();
    static int topicIndex = 0;

    [MenuItem("Assets/Import HTML")]
    private static void ImportHTML()
    {
    	
    	fillIdentifiers();
        fillTopics();
        fillTopicNames();
        int i = 0;

        EditorWebRequest.Requests = new UnityWebRequest[EditorWebRequest.Topics.Count];

        foreach (string topic in Topics)
        {
            Debug.Log("Topic = " + topic); 
            Urls.Add("https://docs.climateinteractive.org/projects/en-roads/en/latest/guide/" + topic + ".html");      
            EditorWebRequest.Requests[i] = UnityWebRequest.Get(EditorWebRequest.Urls[i]);
            EditorWebRequest.Requests[i].SendWebRequest();
            i++;
        }

        EditorApplication.update += EditorWebRequest.WebRequestUpdate;
    }


    public static void WebRequestUpdate()
    {
        Debug.Log("WebRequestUpdate");
        //Debug.Log("Url = " + Urls[topicIndex]);
        bool done = true;
        foreach (UnityWebRequest request in EditorWebRequest.Requests)
        {
            if (!request.isDone)
            {
                done = false;
                break;
            }
        }

        if (!done)
        {
            //Debug.Log("fetching html...");
            return;
        }
        else
        {
            foreach (UnityWebRequest request in EditorWebRequest.Requests)
            {
                if (request.isNetworkError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Debug.Log("Handler.Txt " + request.downloadHandler.text);

                    Htmls.Add(request.downloadHandler.text);
                    string firstIdentifier = "/></a> " + TopicNames[topicIndex] + "<a class=";
                    Debug.Log("IdentifiersOld: " + Identifiers[0]);
                    Identifiers.Insert(0,firstIdentifier);
                    Debug.Log("Identifiers: " + Identifiers[0]);
                    EditorWebRequest.createDataObject(request.downloadHandler.text);
                }
                Identifiers.Remove(Identifiers[0]);
                topicIndex++;
            }
        }

        EditorApplication.update -= EditorWebRequest.WebRequestUpdate;
    }


    public static void createDataObject(string html)
    {
        Debug.Log("createDataObject " + Topics[topicIndex]);
        int i = 0;

        //Debug.Log("Identifiers Length = " + Identifiers.Count);

        foreach (string id in Identifiers)
        {
        	Debug.Log("id = " + id);

        	if (html.Contains(id))
        	{
	            string targetStart = id;
                string targetEnd;
	            int index = html.IndexOf(targetStart);
	            Debug.Log("HTML = " + html);
	            string htmlPart = html.Substring(index);

                if (i == 0) // Information is depicted differently from other content
                {
                    if (htmlPart.Contains("<p>"))
                    {
                        Debug.Log("<p> contained");
                        targetStart = "<p>";
                        index = htmlPart.IndexOf(targetStart);
                    
                        htmlPart = htmlPart.Substring(index);

                    }

                    targetEnd = "</p>";
                }

                else
                {
                    if (htmlPart.Contains("<li>"))
                    {
                        targetStart = "<li>";
                        index = htmlPart.IndexOf(targetStart);
                    
                        htmlPart = htmlPart.Substring(index);

                    }

                    targetEnd = "</div>";

                }

                index = htmlPart.IndexOf(targetEnd);
                htmlPart = htmlPart.Substring(targetStart.Length, index - targetStart.Length);
                
                htmlPart = Regex.Replace(htmlPart, @"<[^>]*>", String.Empty);
                htmlPart = Regex.Replace(htmlPart, @"\r\n?|\n", String.Empty);

                Debug.Log("htmlPart = " + htmlPart);
                

	            fillObjectForJson(i, htmlPart);

      		}

            i++;
            
        }

        createJsonFile();
        EditorWebRequest.objectForJson.ObjectClear();
        //Debug.Log("i = " + i);
        i = 0;

    }

    public static void fillObjectForJson(int index, string html)
    {
        Debug.Log("fillObjectForJson");
        //Debug.Log("Index = " + index);
        switch (index)
        {
            case 0:
                EditorWebRequest.objectForJson.name = Topics[topicIndex];
                EditorWebRequest.objectForJson.information = html;
                Debug.Log("Info = " + EditorWebRequest.objectForJson.information);
                break;
            case 1:
                EditorWebRequest.objectForJson.examples = html;
                Debug.Log("Examples");
                break;
            case 2:
                EditorWebRequest.objectForJson.big_messages = html;
                Debug.Log("Big Messages");
                break;
            case 3:
                EditorWebRequest.objectForJson.key_dynamics = html;
                Debug.Log("Key Dynamics");
                break;
            case 4:
                EditorWebRequest.objectForJson.potential_co_benefits = html;
                Debug.Log("Potential Co-Benefits");
                break;
            case 5:
                EditorWebRequest.objectForJson.equity_considerations = html;
                Debug.Log("Equity Considerations");
                break;
            default:
                Debug.Log("Incorrect Index");
                break;
        }
    }

    public static void createJsonFile()
    {
        Debug.Log("createJsonFile");
        string path = "Assets/Resources/Test2/" + Topics[topicIndex] + ".txt";
        File.Delete(path);
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(JsonUtility.ToJson(EditorWebRequest.objectForJson));
        writer.Close();        
    }


    public static void fillIdentifiers()
    {
        Debug.Log("fillIdentifiers");
        Identifiers.Add("class=\"section\" id=\"examples\">");
        Identifiers.Add("class=\"section\" id=\"big-message\">");
        Identifiers.Add("class=\"section\" id=\"key-dynamics\">");
        Identifiers.Add("class=\"section\" id=\"potential-co-benefits");
        Identifiers.Add("class=\"section\" id=\"equity-considerations\">");
    }

    public static void fillTopics()
    {
        Debug.Log("fillTopics");
        Topics.Add("coal");
        Topics.Add("oil");
        Topics.Add("gas");
        Topics.Add("bioenergy");
        Topics.Add("renewables");
        Topics.Add("nuclear");
        Topics.Add("newtech");
        Topics.Add("carbonprice");
        Topics.Add("transport_ee");
        Topics.Add("transport_elec");
        Topics.Add("buildings_ee");
        Topics.Add("buildings_elec");
        Topics.Add("population");
        Topics.Add("econ_growth");
        Topics.Add("methane");
        Topics.Add("deforestation");
        Topics.Add("afforestation");
        Topics.Add("tech_removal");
    }

    public static void fillTopicNames()
    {
        Debug.Log("fillTopicNames");
        TopicNames.Add("Coal");
        TopicNames.Add("Oil");
        TopicNames.Add("Natural Gas");
        TopicNames.Add("Bioenergy");
        TopicNames.Add("Renewables");
        TopicNames.Add("Nuclear");
        TopicNames.Add("New Technology");
        TopicNames.Add("Carbon Price");
        TopicNames.Add("Transport – Energy Efficiency");
        TopicNames.Add("Transport – Electrification");
        TopicNames.Add("Buildings and Industry - Energy Efficiency");
        TopicNames.Add("Buildings and Industry - Electrification");
        TopicNames.Add("Population Growth");
        TopicNames.Add("Economic Growth");
        TopicNames.Add("Methane &amp; Other Gases");
        TopicNames.Add("Deforestation");
        TopicNames.Add("Afforestation");
        TopicNames.Add("Technological Carbon Dioxide Removal");
    }

}
