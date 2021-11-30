using System.Collections;
using System.Collections.Generic;

// This script creates and fills a dictionary with all correlations between the Content Cards (rooms and panels, respectively).
// The script ActivateCorrelationFrames uses this dictionary to draw frames around the Content Cards
// which correlate to the Content Card that the user has clicked on the interactive table.

public class CorrelationDictionary
{
    // a variable of the type Dictionary,
    // holding a string (Content Card/panel name) and an array of strings (correlative Content Card/panel names)
    public Dictionary<string,string[]> correlationDictionary;

    // method that creates and fills the correlation dictionary
    public CorrelationDictionary()
    {
        // a new instance of dictionary is created
        this.correlationDictionary = new Dictionary<string, string[]>();

        // dictionary is filled with entries holding a Content Card name and its correlative Content Card names:

        // correlative Content Cards to Carbon Removal
        this.correlationDictionary.Add("Carbon Removal", new string[]{"Carbon Pricing", "Deforestation"});

        // correlative Content Cards to Emissions
        this.correlationDictionary.Add("Methane & Co", new string[] {"Deforestation", "Natural Gas"});
        this.correlationDictionary.Add("Deforestation", new string[] {"Methane & Co", "Bioenergy", "Coal", "Natural Gas", "Nuclear", "Oil", "Renewables", "Technological Carbon Removal", "T Electrification", "Afforestation"});

        // correlative Content Cards to Energy Supply
        this.correlationDictionary.Add("Energy Supply", new string[] {"BI Efficiency", "BI Electrification","Carbon Pricing", "G Economic", "G Population", "T Efficiency", "T Electrification"});
        this.correlationDictionary.Add("Bioenergy", new string[]{"Deforestation"});
        this.correlationDictionary.Add("Coal", new string[] {"Bioenergy", "Natural Gas", "New Technologies", "Nuclear", "Renewables", "Oil"});
        this.correlationDictionary.Add("Natural Gas", new string[]{"Coal", "New Technologies", "Nuclear", "Oil", "Renewables"});
        this.correlationDictionary.Add("Oil", new string[] {"Bioenergy", "Natural Gas", "Renewables"});
        this.correlationDictionary.Add("Renewables", new string[] {"Bioenergy", "Nuclear", "Coal"});

        // correlative Content Cards to Transport
        this.correlationDictionary.Add("Transport", new string[] {"Carbon Pricing"}); 
        this.correlationDictionary.Add("T Efficiency", new string[]{"Coal"});
        this.correlationDictionary.Add("T Electrification", new string[]{"Oil"});

        // correlative Content Cards to Buildings and Industry
        this.correlationDictionary.Add("BI Efficiency", new string[]{"Coal"});
    }
}
