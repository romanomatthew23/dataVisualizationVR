﻿using UnityEngine;
using System.Collections;

public class changeReticleColor : MonoBehaviour {
	public GameObject reticle;

	public void changePlaneColor(int value) {
		switch(value)
		{
		case 1: 
			reticle.GetComponent<Renderer> ().material.color = Color.blue;
			break;

		case 2:
			reticle.GetComponent<Renderer> ().material.color = Color.red;
			break;

		case 3:
			reticle.GetComponent<Renderer> ().material.color = Color.yellow;
			break;

		default:
			reticle.GetComponent<Renderer> ().material.color = Color.white;
			break;
		}
	}
}
