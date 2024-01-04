using UnityEngine;
using System.Collections.Generic;

public class GetSliderValues : MonoBehaviour
{
    // collects the slider values from the url and puts them into a dictionary which is later send to rest api to get corresponding temperatures (see RestAPICallTest.cs).
    public Dictionary<string, string> GetQueryParameters(string url)
    {
        Dictionary<string, string> query = new Dictionary<string, string>();
        int questionMarkIndex = url.IndexOf('?');

        //Debug.Log("getslidervalues url: " + url);

        if (questionMarkIndex >= 0)
        {
            string queryString = url.Substring(questionMarkIndex + 1);
            string[] parameters = queryString.Split('&');
            foreach (string parameter in parameters)
            {
                string[] parts = parameter.Split('=');

                if (parts.Length == 2)
                {
                    string key = parts[0];
                    
                    if (key.Contains("p"))
                    {
                        key = key.Replace("p", "");
                    }
                    
                    string value = parts[1];
                    query.Add(key, value);
                    
                    //Debug.Log("key: " + key);
                }
            }
        }

        return query;
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // old inactive methods (before nov/dec 2023): we had to apply further formatting to the dictionary returned from GetQueryParameters to pass it to en_roads.exe
    public string getter(string url)
    {
        string valuesForAPI = "";
        Dictionary<string, string> query = GetQueryParameters(url);
        Dictionary<int, int> mapping = LoadIndexMapping();

        for (int i = 1; i <= 284; i++)
        {
            if (query.ContainsKey("p" + i))
            {
                string value = query["p" + i];
                if (mapping.ContainsKey(i))
                {
                    valuesForAPI += mapping[i] + ":" + value + " ";
                }
            }
        }
        return valuesForAPI.Trim();
    }

    // old inactive methods (before nov/dec 2023): we had to apply further formatting to the dictionary returned from GetQueryParameters to pass it to en_roads.exe
    private Dictionary<int, int> LoadIndexMapping()
    {
        Dictionary<int, int> mapping = new Dictionary<int, int>();
        TextAsset csvFile = Resources.Load<TextAsset>("index_mapping");
        string[] lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(';');
            if (fields.Length == 2)
            {
                int index = int.Parse(fields[1]);
                int value = int.Parse(fields[0]);
                mapping.Add(index, value);
            }
        }
        return mapping;
    }
}