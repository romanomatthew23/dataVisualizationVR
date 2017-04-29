using UnityEngine;
using System.Collections;

public class Floor_Color_Changer : MonoBehaviour {
	public GameObject plane;

	public void changePlaneColor(int value) {
		switch(value)
		{
		case 1: 
			plane.GetComponent<Renderer> ().material.color = Color.blue;
			break;

		case 2:
			plane.GetComponent<Renderer> ().material.color = Color.red;
			break;

		case 3:
			plane.GetComponent<Renderer> ().material.color = Color.yellow;
			break;

		default:
			plane.GetComponent<Renderer> ().material.color = Color.white;
			break;
		}
	}
}
