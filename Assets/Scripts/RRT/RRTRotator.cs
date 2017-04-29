﻿using UnityEngine;
using System.Collections;

public class RRTRotator : MonoBehaviour {
	public GameObject graph;
	public GameObject menu;
	public GameObject overlay;
	// Update is called once per frame
	void Update () {
		execRot ();
	}

	void execRot() {
		if (Input.GetKeyDown (KeyCode.JoystickButton5)) {	//xbox 'right bumper" button
			graph.transform.Rotate (0, 10, 0);
			Debug.Log ("Rotate Right");
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton4)) {	//xbox 'left bumper" button
			graph.transform.Rotate (0, -10, 0);
			Debug.Log ("Rotate Left");
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton7)) {	//xbox 'start" button
			menu.SetActive (true);
			overlay.SetActive (false);
			graph.SetActive(false);
		}
	}
}