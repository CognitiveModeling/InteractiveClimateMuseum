using System.Collections;
using System.Collections.Generic;


public class CorrelationDictionary
{
    public Dictionary<string,string[]> correlationDictionary;

    public CorrelationDictionary()
    {
        this.correlationDictionary = new Dictionary<string, string[]>();
 
        this.correlationDictionary.Add("Carbon Removal", new string[]{"Carbon Pricing", "Deforestation"});

        this.correlationDictionary.Add("Methane & Co", new string[] {"Deforestation", "Natural Gas"});
        this.correlationDictionary.Add("Deforestation", new string[] {"Methane & Co", "Bioenergy", "Coal", "Natural Gas", "Nuclear", "Oil", "Renewables", "Technological Carbon Removal", "T Electrification", "Afforestation"});

        this.correlationDictionary.Add("Energy Supply", new string[] {"BI Efficiency", "BI Electrification","Carbon Pricing", "G Economic", "G Population", "T Efficiency", "T Electrification"});
        this.correlationDictionary.Add("Bioenergy", new string[]{"Deforestation"});
        this.correlationDictionary.Add("Coal", new string[] {"Bioenergy", "Natural Gas", "New Technologies", "Nuclear", "Renewables", "Oil"});
        this.correlationDictionary.Add("Natural Gas", new string[]{"Coal", "New Technologies", "Nuclear", "Oil", "Renewables"});
        this.correlationDictionary.Add("Oil", new string[] {"Bioenergy", "Natural Gas", "Renewables"});
        this.correlationDictionary.Add("Renewables", new string[] {"Bioenergy", "Nuclear", "Coal"});

        this.correlationDictionary.Add("Transport", new string[] {"Carbon Pricing"}); 
        this.correlationDictionary.Add("T Efficiency", new string[]{"Coal"});
        this.correlationDictionary.Add("T Electrification", new string[]{"Oil"});

        this.correlationDictionary.Add("BI Efficiency", new string[]{"Coal"});
    }

}
