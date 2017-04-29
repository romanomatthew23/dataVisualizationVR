using UnityEngine;
using System.Collections;

public class backToMenu : MonoBehaviour {
	public GameObject welcome, menu, data, audio, options, video;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.JoystickButton7)) {
			welcome.SetActive (false);
			menu.SetActive (true);
			data.SetActive (false);
			audio.SetActive (false);
			options.SetActive (false);
			video.SetActive (false);
		}
	
	}
}
