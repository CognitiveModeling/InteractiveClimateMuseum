using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectForJson
{
	public string name;
	public string information;
   	public string examples;
	public string big_messages;
	public string key_dynamics;
	public string potential_co_benefits;
	public string equity_considerations;
	public string correlations;


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


	public static ObjectForJson FromTextAsset(TextAsset json)
	{
		Debug.Log("new json object with content: " + json.text);
		return new ObjectForJson("a", "a", "a", "a", "a", "a", "a", "a");
	}

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