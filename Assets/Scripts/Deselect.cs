using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Deselect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
			GameObject myEventSystem = GameObject.Find ("EventSystem");
			//myEventSystem.currentSelectedGameObject = null;
			//myEventSystem.
			myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
		}
	}

	public void deSelect() {
		GameObject myEventSystem = GameObject.Find ("EventSystem");
		//myEventSystem.currentSelectedGameObject = null;
		//myEventSystem.
		myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (null);
	}
}
