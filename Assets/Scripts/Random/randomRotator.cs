﻿using UnityEngine;
using System.Collections;

public class randomRotator : MonoBehaviour {
	public float speed = 1.0F;
	public GameObject graph;
	public GameObject menu;
	public GameObject overlay;


	// Update is called once per frame
	void Update () {
		execRot ();
	}

	void execRot() {
		if (Input.GetKeyDown (KeyCode.JoystickButton5)) {	//xbox 'right bumper" button
			rotateRight();
		}

		if (Input.GetKeyDown (KeyCode.JoystickButton4)) {	//xbox 'left bumper" button
			rotateLeft();
		}
		if (Input.GetKeyDown (KeyCode.JoystickButton7)) {	//xbox 'start" button
			exitRandom();
		}
	}

	public void rotateRight() {
		graph.transform.Rotate (0, 10, 0);
		Debug.Log ("Rotate Right");
	}

	public void rotateLeft() {
		graph.transform.Rotate (0, -10, 0);
		Debug.Log ("Rotate Left");
	}

	public void exitRandom() {
		menu.SetActive (true);
		graph.SetActive(false);
		overlay.SetActive (false);
	}
}
