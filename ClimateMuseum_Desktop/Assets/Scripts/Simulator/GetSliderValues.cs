using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GetSliderValues : MonoBehaviour
{
    [SerializeField] private double p1;
    [SerializeField] private double p7;
    [SerializeField] private double p10;
    [SerializeField] private double p16;
    [SerializeField] private double p23;
    [SerializeField] private double p30;
    [SerializeField] private double p35;
    [SerializeField] private double p39;
    [SerializeField] private double p47;
    [SerializeField] private double p50;
    [SerializeField] private double p53;
    [SerializeField] private double p55;
    [SerializeField] private double p57;
    [SerializeField] private double p59;
    [SerializeField] private double p63;
    [SerializeField] private double p235;
    [SerializeField] private double p65;
    [SerializeField] private double p67;

    private string apiUrl = "https://en-roads.climateinteractive.org/scenario.html?v=23.2.1&p1=39&p30=-0.04&p35=2&p39=157&p47=4.1&p50=5&p53=65&p55=62&p57=-4.4&p59=-63&p63=11.7&p235=2.1&p65=46&p67=31";

    void Start()
    {
        getter(apiUrl);
    }
    public void getter(string url)
    {
        // Get the values of the parameters from the URL
        var parameters = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query);

        // Loop through the parameters and store the values in the corresponding public variable
        foreach (var parameterName in parameters.AllKeys.Where(k => k.StartsWith("p")))
        {
            double value;
            if (double.TryParse(parameters[parameterName], out value))
            {
                switch (parameterName)
                {
                    case "p1":
                        p1 = value;
                        break;
                    case "p7":
                        p7 = value;
                        break;
                    case "p10":
                        p10 = value;
                        break;
                    case "p16":
                        p16 = value;
                        break;
                    case "p23":
                        p23 = value;
                        break;
                    case "p30":
                        p30 = value;
                        break;
                    case "p35":
                        p35 = value;
                        break;
                    case "p39":
                        p39 = value;
                        break;
                    case "p47":
                        p47 = value;
                        break;
                    case "p50":
                        p50 = value;
                        break;
                    case "p53":
                        p53 = value;
                        break;
                    case "p55":
                        p55 = value;
                        break;
                    case "p57":
                        p57 = value;
                        break;
                    case "p59":
                        p59 = value;
                        break;
                    case "p63":
                        p63 = value;
                        break;
                    case "p235":
                        p235 = value;
                        break;
                    case "p65":
                        p65 = value;
                        break;
                    case "p67":
                        p67 = value;
                        break;
                }
            }
        }
    }
}