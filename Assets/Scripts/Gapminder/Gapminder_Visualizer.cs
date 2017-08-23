using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System;
using System.Collections.Generic;

public class Gapminder_Visualizer : MonoBehaviour {
	//prefab data point
	public GameObject sphere;

	//way to get back to the menu
	public GameObject menu;

	public GameObject graph;

	//mins and maxes
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	//year displayed
	public int year = 1951;

	//width and height of world graph
	public float xWorldMax = 10;
	public float yWorldMax = 10;
	public float xWorldOffset = -5;

	//JSON STUFF
	private string jsonString;
	private JsonData itemDataOne;
	private JsonData itemDataTwo;

	//which indicators to select variables
	private int indicatorTwo=0;
	private int indicatorOne=0;

	//differnces
	private float xDiff;
	private float yDiff;

	private int usIdx = -1;

	//holds filenames in fancy array
	private string[] filenames = {"","CO2 emission total","CO2 emission per capita","Children per woman","Child mortality per 1000","GDP per capita","Life Expectancy"};
									//74 countries in CO2 emissions
	private string[] countries = new string[] {"Afghanistan","Albania","Algeria","Angola","Antigua and Barbuda","Argentina","Armenia","Australia","Austria","Azerbaijan","Bahamas",
		"Bahrain","Bangladesh","Barbados","Belarus","Belgium",
		"Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Brazil","Brunei","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon",
		"Canada","Cape Verde","Cayman Islands","Central African Republic","Chile","China","Colombia","Comoros","Congo, Dem. Rep.","Congo, Rep.","Costa Rica","Cote d'Ivoire","Croatia",
		"Cuba","Cyprus","Czech Republic","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Ethiopia","Faeroe Islands","Fiji","Finland","France",
		"French Guiana","Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guinea","Guinea-Bissau","Guadeloupe","Guatemala","Guinea-Bissau","Guyana","Haiti","Honduras",
		"Hong Kong, China","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Israel","Italy","Jamaica","Japan","Jordan","Kazakhstan","Kenya","Kiribati","Kuwait","Kyrgyz Republic","Lao","Latvia","Lebanon","Lesotho","Liberia","Libya",
		"Lithuania","Luxembourg","North Korea","Macao, China","Macedonia, FYR","Madagascar","Malawi","Malaysia","Maldives","Mali",
		"Malta", "Marshall Islands","Mauritania","Martinique","Mauritius","Micronesia, Fed. Sts.","Mexico","Mongolia","Monaco","Montenegro","Morocco","Mozambique","Moldova","Myanmar","Namibia","Nauru",
		"Nepal","Netherlands","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Sao Tome and Principe","Slovenia", "Solomon Islands","Somalia",
		"Norway","Oman","Pakistan","Palau","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal","Puerto Rico","Qatar","Reunion","Romania","Russia","Rwanda","Slovak Republic","St. Kitts and Nevis",
		"St. Lucia","St. Vincent and the Grenadines","San Marino","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore",
		"South Africa","Spain","Sri Lanka","Sudan","Suriname","Swaziland","Sweden","Switzerland","Syria","Taiwan","Tajikistan","Tanzania","Thailand","Timor-Leste","Togo","Tonga","Trinidad and Tobago",
		"Tunisia","Turkey","Turkmenistan","Turks and Caicos Islands","Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom","United States","Uruguay","Uzbekistan","Vanuatu","Venezuela","West Bank and Gaza",
		"Vietnam","Yemen","Zambia","Zimbabwe"};
	
	//private IList countryOverlap;
	private string[] countryOverlap;

	private bool traverseTimeEngaged = false;
	private int counter =0;
	private bool initialized;
	//Variables to hold Unity Game Objects, Data, and other values
	private int n;
	private int len;
	//private IList points;
	//private List<Vector3> points;
	private Vector3[] points;
	private GameObject[] objects;
	public bool generated;

	void Start () {
		initialized = false;
	}

	void Update () {
		if (initialized == false)
			init ();

		if (Input.GetKeyDown (KeyCode.JoystickButton3)) {	//xbox "y" button
			loadData();
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton2)) {	//xbox 'x" button
			destroyObjects ();
		}

		if ( (Input.GetKey (KeyCode.JoystickButton5) && (counter % 10 == 2))  || traverseTimeEngaged ) {	//xbox 'right bumper" button
			if(generated)
				advanceBalls(true);
		}

		else if (/*Input.GetKeyDown (KeyCode.JoystickButton4)*/ Input.GetKey (KeyCode.JoystickButton4) && (counter % 10 == 2) ) {	//xbox 'left bumper" button
			if(generated)
				advanceBalls(false);
		}



		if (Input.GetKeyDown (KeyCode.JoystickButton7)) {	//xbox 'start" button
			//clean up and get back to start
			destroyObjects();
			//transform.position = new Vector3(-50, 2, 50);
			menu.SetActive (true);
			graph.SetActive(false);
		}

		if (generated) {
			interpolate ();
		}

		counter++;
	}

	/***************************************************************************************************************************
	 * 
	 * 													Start Public Functions
	 * 		
	 * 
	 * **************************************************************************************************************************/

	public void displayPoints() {
		loadData();
	}

	public void traverseTime() {
		traverseTimeEngaged = true;
	}

	public void deletePoints() {
		traverseTimeEngaged = false;
		destroyObjects();
	}

	public void exitGapminder() {
		//clean up and get back to start
		traverseTimeEngaged = false;
		destroyObjects();
		//transform.position = new Vector3(-50, 2, 50);
		menu.SetActive (true);
		graph.SetActive(false);
	}


	/***************************************************************************************************************************
	* 
	* 													End Public Functions
	* 		
	* 
	* **************************************************************************************************************************/

	void interpolate() {
		for (int i = 0; i < n; i++) {
			float speed = 1;
			float step = speed * Time.deltaTime;
			objects [i].transform.localPosition = Vector3.MoveTowards (objects [i].transform.localPosition, points [i], step);
		}
	}



	void advanceBalls(bool right) {
		if (right) {
			if (year >= 2009)
				return;
			else
				year++;
		} else {
			if (year <= 1951)
				return;
			else
				year--;
		}

		Debug.Log ("year = " + year);
		for (int i = 0; i < n; i++) {
			float x = 0;
			float y = 0;
			try {
				string country = countries[i];
				//x = (float)((System.Double)itemDataOne[country] [year.ToString ()] / 10000.0);
				x = (float)((System.Double)itemDataOne[country] [year.ToString ()]);
				try {
					//y = (float)((System.Double)itemDataTwo[country] [year.ToString ()] / 10000.0);
					y = (float)((System.Double)itemDataTwo[country] [year.ToString ()]);
				} catch(Exception e) {
					continue;
				}
			} catch(Exception e) {
				continue;
			}
			x = ((x - xMin) / xDiff * xWorldMax) + xWorldOffset;
			y = ((y - yMin) / yDiff * yWorldMax);
			Vector3 tempPoint = new Vector3 (x, y, 0);
			points [i].Set (x, y, (float) 0);
			//objects [i].transform.localPosition = tempPoint;
		}
	}

	void init() {
		generated = false;
		n = 0;
		len = countries.GetLength (0);
		countryOverlap = new string[len];
		points = new Vector3[len];
		objects = new GameObject[len];
		initialized = true;
	}

	void loadData() {
		if ( (indicatorOne!=0) && (indicatorTwo!=0) && (!generated) ) {		//might want to change this later of when we actually load it
			//load the data if both nonzero (don't display it yet)
			loadIndicatorOneData ();
			loadIndicatorTwoData ();

			loadObjects ();
		} else
			return;
	}
		
	void loadObjects () {
		//use try catch later if you want to try and access members you cant
		//number of points is the number of countries
		//int n=75;
		year = 1951;
		n = 0;

		sphere.transform.localPosition = new Vector3 (0, 0, 0);


		///Debug.Log (countries.GetLength(0));
		for (int i = 0; i < len; i++) {
			countryOverlap [i] = "";
			points [i].Set (0, 0, 0);
		}

		//objects = new GameObject[204];
		int idx = 0;
		xMin = 100000;
		xMax = 0;
		yMin = 100000;
		yMax = 0;
		//Step 1 (really now) see what countries overlap 
		for(int i=0; i < countries.GetLength(0); i++) {
			float x=0;
			float y=0;
			string country = countries[i];
			try {
				//x = (float)((System.Double)itemDataOne[country] [year.ToString ()] / 10000.0);
				x = (float)((System.Double)itemDataOne[country] [year.ToString ()]);
				try {
					//y = (float)((System.Double)itemDataTwo[country] [year.ToString ()] / 10000.0);
					y = (float)((System.Double)itemDataTwo[country] [year.ToString ()]);
				} catch(Exception e) {
					continue;
				}
			} catch(Exception e) {
				continue;
			}
			if(country == "United States")
				usIdx = idx;


			//if we get here then both indicators have the given country!! :)
			//countryOverlap
			//points.Add(new Vector3(x,y,(float) 0));
			points [idx].Set (x, y, (float) 0);
			//countryOverlap.Add (country);
			countryOverlap[idx] = country;
			idx++;
	
			//get the min and max values for later
			if (x < xMin)
				xMin = x;
			if (x > xMax)
				xMax = x;
			if (y < yMin)
				yMin = y;
			if (y > yMax)
				yMax = y;
		}
		n = idx + 1;		//n is the size of the set of countries that overlap (1+ idx)

		//Step 2 Resize The data so that it fits within the graph 
		//subtract away the minimum value
		//divide by the difference between max and min
		//multiply by the max height you want in world space
		xDiff = xMax - xMin;
		yDiff = yMax - yMin;

		for (int i = 0; i < n; i++) {
			points[i].x = ( (points[i].x - xMin) / xDiff * xWorldMax ) + xWorldOffset;
			points[i].y = (points[i].y - yMin) / yDiff * yWorldMax;
		}


		for (int i = 0; i < n; i++) 
		{	
			objects [i] = (GameObject) Instantiate (sphere);
			objects [i].transform.SetParent (transform);
			objects [i].transform.localPosition = points [i];
			if(i == usIdx)
				objects [i].GetComponent<Renderer> ().material.color = Color.red;
		}

		for (int i = 0; i < n; i++) 
		{
			objects [i].SetActive (true);
		}

		generated = true;
	}


	//functions to load json objects based on indicators
	void loadIndicatorOneData() {
		// parse json file
		jsonString = File.ReadAllText (Application.dataPath + "/VisualData/" + filenames[indicatorOne] + ".json");
		itemDataOne = JsonMapper.ToObject (jsonString);
	}
	void loadIndicatorTwoData() {
		// parse json file
		jsonString = File.ReadAllText (Application.dataPath + "/VisualData/" + filenames[indicatorTwo] + ".json");
		itemDataTwo = JsonMapper.ToObject (jsonString);
	}


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
			if(objects[i] != null)
				Destroy (objects [i]);
		}
		generated = false;

		//countryOverlap
		//points;


	}


	// Public Functions to change indicator values based on dropdown menu
	public void updateIndicatorOne(int value) {
		indicatorOne = value;
	}

	public void updateIndicatorTwo(int value) {
		indicatorTwo = value;
	}



}
