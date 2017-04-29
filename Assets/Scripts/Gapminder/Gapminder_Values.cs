using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gapminder_Values : MonoBehaviour {
	public GameObject graph;
	public Text mark0y, mark1y, mark2y, mark3y, mark4y, mark5y;
	public Text mark0x, mark1x, mark2x, mark3x, mark4x, mark5x;
	public Text yearValue;
	//mark0y


	private Gapminder_Visualizer script;
	// Use this for initialization
	void Start () {
		script = graph.GetComponent<Gapminder_Visualizer> ();
	}
	
	// Update is called once per frame
	void Update () {
		yearValue.text = script.year.ToString();

		if (script.generated) {
			float xMin = script.xMin;
			float xMax = script.xMax;
			float yMin = script.yMin;
			float yMax = script.yMax;

			float xStep = (xMax - xMin) / 5;
			float yStep = (yMax - yMin) / 5;

			mark0x.text = xMin.ToString ();
			mark1x.text = (xMin + xStep).ToString ();
			mark2x.text = (xMin + 2*xStep).ToString ();
			mark3x.text = (xMin + 3*xStep).ToString ();
			mark4x.text = (xMin + 4*xStep).ToString ();
			mark5x.text = (xMax).ToString ();

			mark0y.text = yMin.ToString ();
			mark1y.text = (yMin + yStep).ToString ();
			mark2y.text = (yMin + 2*yStep).ToString ();
			mark3y.text = (yMin + 3*yStep).ToString ();
			mark4y.text = (yMin + 4*yStep).ToString ();
			mark5y.text = (yMax).ToString ();

		}
	}
}
