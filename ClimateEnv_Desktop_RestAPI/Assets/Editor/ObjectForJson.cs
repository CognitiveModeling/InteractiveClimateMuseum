using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is not a class, but a struct and does ...
// It is not assigned to any object in the editor.

// Line 12 indicates that this struct can be serialized.
// ("Serialization is the automatic process of transforming data structures or object states into a format
// that Unity can store and reconstruct later.", https://docs.unity3d.com/Manual/script-Serialization.html)

[System.Serializable]
public struct ObjectForJson
{
    // strings for the panel name and each child/sub tab name in the panel
    public string name;
    public string information;
    public string examples;
    public string big_messages;
    public string key_dynamics;
    public string potential_co_benefits;
    public string equity_considerations;
    public string correlations;

    // constructor
    public ObjectForJson(string name, string information, string examples,
        string big_messages, string key_dynamics,
        string potential_co_benefits, string equity_considerations,
        string correlations)
    {
        this.name = name;
        this.information = information;
        this.examples = examples;
        this.big_messages = big_messages;
        this.key_dynamics = key_dynamics;
        this.potential_co_benefits = potential_co_benefits;
        this.equity_considerations = equity_considerations;
        this.correlations = correlations;
    }

    // empties all strings
    public void ObjectClear()
    {
        this.name = "";
        this.information = "";
        this.examples = "";
        this.big_messages = "";
        this.key_dynamics = "";
        this.potential_co_benefits = "";
        this.equity_considerations = "";
        this.correlations = "[]";
    }
}