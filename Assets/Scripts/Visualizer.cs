using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Visualizer : MonoBehaviour {
	public GameObject sphere;			//prefab data point
	private string jsonString;
	private JsonData itemData;


	private int n = 10;			//# of data points 
	private Vector3[] points;
	private GameObject[] objects;
	private bool generated;
	// Use this for initialization
	void Start () {
		generated = false;

		// parse json file
		jsonString = File.ReadAllText (Application.dataPath + "/VisualData/data.json");
		//Debug.Log (jsonString); 
		itemData = JsonMapper.ToObject (jsonString);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
		}

		if (Input.GetKeyDown (KeyCode.Delete)) {
			Debug.Log ("Started Destruction of Spheres");
			destroyObjects ();

		}

		if (Input.GetKeyDown (KeyCode.L)) {
			Debug.Log ("Load points from json file");
			loadByType ("planet");

		}
	
	}

/// <Random Data Set Functions>


	void dataRand() {
		randomizePoints ();
		initializeObjects ();
		displayObjects ();
	}



	void randomizePoints()
	{
		points = new Vector3[n];
		for (int i = 0; i < n; i++) {
			points [i].Set ((Random.value - .5F) * 10, (Random.value) * 5, (Random.value - .5F) * 10);
		}
	}

	void initializeObjects()
	{
		if (generated) {
			return;
		}
		objects = new GameObject[n];
		for(int i=0; i < n; i++)
		{
			//objects [i] = (GameObject) Instantiate (sphere, points [i], Quaternion.identity);
			objects [i] = (GameObject) Instantiate (sphere);
			objects [i].transform.SetParent (transform);
			objects [i].transform.localPosition = points [i];
		}
		generated = true;
	}

/// </Random Data Set Functions>

	void displayObjects()
	{
		if (!generated) {
			return;
		}
		for (int i = 0; i < n; i++) 
		{
			objects [i].SetActive (true);
		}
	}

	void destroyObjects()
	{
		if (!generated) {
			return;
		}
		for (int i = 0; i < n; i++) 
		{
			Destroy (objects [i]);
		}
		generated = false;
	}

	void loadByType (string type)
	{
		int count = itemData [type].Count;
		float test = (float)(System.Double) itemData [type] [1] ["position"] ["x"];
		Debug.Log (test);
		Debug.Log (count);

		for (int i = 0; i < count; i++) {
			Debug.Log ((float)(System.Double) itemData [type] [i] ["position"] ["z"]);
			points [i].Set ((float)(System.Double) itemData [type] [i] ["position"] ["x"]/6, (float)(System.Double) itemData [type] [i] ["position"] ["y"]/6, (float)(System.Double) itemData [type] [i] ["position"] ["z"]/6);
		}
		if (generated) {
			return;
		}

		// initialize gameObjects
		objects = new GameObject[count];
		for(int i=0; i < count; i++)
		{
			//objects [i] = (GameObject) Instantiate (sphere, points [i], Quaternion.identity);

			objects [i] = (GameObject) Instantiate (sphere);
			objects [i].transform.localPosition = points [i];
			objects [i].transform.SetParent (transform);
		}
		generated = true;

		// display gameObjects
		if (!generated) {
			return;
		}
		for (int i = 0; i < count; i++) 
		{
			Debug.Log ("here");
			objects [i].SetActive (true);
		}

	}

}
