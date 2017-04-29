using UnityEngine;
using System.Collections;

public class VolumeControl : MonoBehaviour {
	//public AudioSource shakeItOff, blankSpace, badBlood;
	public GameObject shakeItOff, blankSpace, badBlood;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void muteAudio() {
		AudioListener.volume = 0.0F;
	}

	public void unMuteAudio() {
		AudioListener.volume = 1.0F;
	}

	public void setVolume(float volume) {
		AudioListener.volume = volume;
	}

	public void muteMe(bool muted) {
		if(muted)
			AudioListener.volume = 0.0F;
		else
			AudioListener.volume = 1.0F;
	}

	public void changeMusic(int value) {
		switch(value)
		{
		case 1: 
			shakeItOff.SetActive(false);
			blankSpace.SetActive(true);
			badBlood.SetActive(false);
			Debug.Log ("case 1: blankSpace enabled!");
			break;

		case 2:
			shakeItOff.SetActive(true);
			blankSpace.SetActive(false);
			badBlood.SetActive(false);
			Debug.Log ("case 2 Shake it off enabled!");
			break;

		case 3:
			shakeItOff.SetActive(false);
			blankSpace.SetActive(false);
			badBlood.SetActive(true);
			Debug.Log ("case 3: badBlood enabled!");
			break;

		default:
			shakeItOff.SetActive(false);
			blankSpace.SetActive(false);
			badBlood.SetActive(false);
			Debug.Log ("Default case!");
			break;
		}
	}

}
