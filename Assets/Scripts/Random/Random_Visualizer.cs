using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Random_Visualizer : MonoBehaviour {
	public GameObject sphere;			//prefab data point

	private int n = 10;			//# of data points 
	private Vector3[] points;
	private GameObject[] objects;
	private bool generated;
	// Use this for initialization
	void Start () {
		generated = false;
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.JoystickButton3)) {	//xbox "y" button
			dataRand();
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton2)) {	//xbox 'x" button
			destroyObjects ();
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

}
