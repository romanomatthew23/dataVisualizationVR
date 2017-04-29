using UnityEngine;
using System.Collections;

public class RRT_Visualizer : MonoBehaviour {
	public GameObject sphere;			//prefab data point
	public GameObject cylinder;			//prefab for connecting

	private int n = 1000;			//# of data points 
	private Vector3[] samplePoints;
	public float stepSize = 10;

	private int counter = 0;
	private GameObject[] objects;
	private GameObject[] edges;
	private bool generated;
	private bool finished = false;
	private int q=0;	//q always points to an index beyond what you have


	// Use this for initialization
	void Start () {
		generated = false;
	}
		



	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.JoystickButton3)) {	//xbox "y" button
			randomAll();
		}
		*/



		if (generated && (counter % 10 == 2)/*&& Input.GetKeyDown (KeyCode.JoystickButton0) */) {
			//algorithm step by step
			RRTalgo();
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton3)) {	//xbox "y" button
			randInit();
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton2)) {	//xbox 'x" button
			destroyObjects ();
		}
		counter++;
	}

	//step 1 generate a bunch of random points (n points) in Q


	void randomAll() {
		samplePoints = new Vector3[n];
		for (int i = 0; i < n; i++) {
			samplePoints [i].Set ((Random.value - .5F) * 10, (Random.value) * 5, (Random.value - .5F) * 10);
		}


		if (generated) {
			return;
		}
		objects = new GameObject[n];
		for(int i=0; i < n; i++)
		{
			//objects [i] = (GameObject) Instantiate (sphere, points [i], Quaternion.identity);
			objects [i] = (GameObject) Instantiate (sphere);
			objects [i].transform.SetParent (transform);
			objects [i].transform.localPosition = samplePoints [i];
		}
		generated = true;


		if (!generated) {
			return;
		}
		for (int i = 0; i < n; i++) 
		{
			objects [i].SetActive (true);
		}
	}




	/// <Random Data Set Functions>

	void RRTalgo() {
		if (q == n) {
			finished = true;
			return;
		}
		//Debug.Log (q);
		Vector3 xRand = samplePoints [q];
		Vector3 xNear = closestPoint (xRand);
		Vector3 xNew = Vector3.MoveTowards (xNear, xRand, stepSize);
		addToTree (xNew);
		addEdge (xNew, xNear);
		q++;
	}

	//Debug.DrawLine (Vector3

	void addEdge(Vector3 xnew, Vector3 xnear) {
		Vector3 midpoint = (xnew + xnear) / 2;
		edges [q - 1].transform.localPosition = midpoint;

		//oat yScale = Vector3.Distance (xnew, xnear);
		float yScale = 0.5F;
		edges [q - 1].transform.localScale = new Vector3 (0.025F, yScale, 0.025F);

		//find the point with the smallest y
		Vector3 xHigh, xLow;
		if (xnew.y < xnear.y) {
			xLow = xnew;
			xHigh = xnear;
		} else {
			xLow = xnear;
			xHigh = xnew;
		}
	
		Vector3 pointTo = new Vector3 (xHigh.x - xLow.x, 0, xHigh.z - xLow.z);
		Vector3 rotAxis = Vector3.Cross (Vector3.up, pointTo);	//gets vector to turn off of
		float angle = Vector3.Angle(pointTo, xHigh - xLow);
		Debug.Log (angle);
		Quaternion qEdge = Quaternion.AngleAxis (90 - angle, rotAxis);
		edges [q - 1].transform.localRotation = qEdge;



		edges [q - 1].SetActive (true);
	}

	Vector3 closestPoint(Vector3 xRand) {
		Vector3 xClose = objects [0].transform.localPosition;
		float minDist = Vector3.Distance (xClose, xRand);

		for (int i = 0; i < q; i++) {
			Vector3 temp = objects [i].transform.localPosition;
			float dist = Vector3.Distance (temp, xRand);
			if (dist < minDist) {
				//Debug.Log ("new min!");
				xClose = temp;
			}
		}
		return xClose;
	}


	void randInit() {
		if (generated)
			return;
		randomizePoints ();
		initializeObjects ();	//but don't show them
		addToTree (samplePoints [0]);
		generated = true;
		q++;

	}

	void addToTree(Vector3 point) {
		objects [q].transform.localPosition = point;
		objects [q].SetActive (true);
	}

	void randomizePoints()
	{
		samplePoints = new Vector3[n];
		for (int i = 0; i < n; i++) {
			samplePoints [i].Set ((Random.value - .5F) * 10, (Random.value) * 5, (Random.value - .5F) * 10);
		}
	}

	void initializeObjects()
	{
		if (generated) {
			return;
		}
		objects = new GameObject[n];
		edges = new GameObject[n - 1];
		for(int i=0; i < n; i++)
		{
			//objects [i] = (GameObject) Instantiate (sphere, points [i], Quaternion.identity);
			objects [i] = (GameObject) Instantiate (sphere);
			objects [i].transform.SetParent (transform);

			if(i != n-1) {
				edges [i] = (GameObject)Instantiate (cylinder);
				edges [i].transform.SetParent (transform);
				//objects [i].transform.localPosition = samplePoints [i];
			}
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
		if(objects[n-1] != null)
			Destroy (objects [n-1]);
		for (int i = 0; i < n-1; i++) 
		{
			if(objects[i] != null)
				Destroy (objects [i]);
			if (edges [i] != null)
				Destroy (edges [i]);
		}
		generated = false;
		finished = false;
		q = 0;
	}

}
