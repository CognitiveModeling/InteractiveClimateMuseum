using UnityEngine;
using System.Collections.Generic;

public class GetSliderValues : MonoBehaviour
{
    public string getter(string url)
    {
        string valuesForAPI = "";
        Dictionary<string, string> query = GetQueryParameters(url);
        for (int i = 1; i <= 180; i++)
        {
            if (query.ContainsKey("p" + i))
            {
                valuesForAPI += i + ":" + query["p" + i] + " ";
            }
        }
        return valuesForAPI.Trim();
    }

    private Dictionary<string, string> GetQueryParameters(string url)
    {
        Dictionary<string, string> query = new Dictionary<string, string>();
        int questionMarkIndex = url.IndexOf('?');
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
                    string value = parts[1];
                    query.Add(key, value);
                }
            }
        }
        return query;
    }
}