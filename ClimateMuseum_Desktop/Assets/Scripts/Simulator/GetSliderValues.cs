using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GetSliderValues : MonoBehaviour
{
    [SerializeField] private double[] p = new double[181];

    private string apiUrl = "https://en-roads.climateinteractive.org/scenario.html?v=23.2.1";
    string valuesForAPI = "";

    void Start()
    {
        getter(apiUrl);
    }

    public string getter(string url)
    {
        // Get the values of the parameters from the URL
        var parameters = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query);

        // Loop through the parameters and build the valuesForAPI string
        foreach (var parameterName in parameters.AllKeys.Where(k => k.StartsWith("p")))
        {
            double value;
            if (double.TryParse(parameters[parameterName], out value))
            {
                int index;
                if (int.TryParse(parameterName.Substring(1), out index))
                {
                    valuesForAPI += $"{index}:{value} ";
                }
            }
        }

        // Remove the trailing space and return the string
        return valuesForAPI.TrimEnd();
    }
}