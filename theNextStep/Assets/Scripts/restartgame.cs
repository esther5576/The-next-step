using UnityEngine;
using System.Collections;

public class restartgame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetKey (KeyCode.Backspace)) {
			Application.LoadLevel (Application.loadedLevel);
		}

		if (Input.GetMouseButtonDown (2)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
