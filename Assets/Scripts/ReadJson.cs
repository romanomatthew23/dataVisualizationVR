using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class ReadJson : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText (Application.dataPath + "/VisualData/data.json");
		//Debug.Log (jsonString); 
		itemData = JsonMapper.ToObject (jsonString); 
		Debug.Log (itemData["planet"][1]["label"]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
