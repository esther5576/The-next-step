using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class UnityAnaliticsTheNextStep : MonoBehaviour
{
	float _oxygen;
	float _water;
	float _food;


	/*Analytics.CustomEvent ("endGame", new Dictionary<string, object>
	                       {
		{ "Oxygen ", _oxygen },
		{ "Water ", _water },
		{ "Food ", _food }
	});*/
	// Use this for initialization
	void Awake ()
	{
		_oxygen = Camera.main.gameObject.GetComponent<gameStats> ()._actualOxygen;
		_water = Camera.main.gameObject.GetComponent<gameStats> ()._actualWater;
		_food = Camera.main.gameObject.GetComponent<gameStats> ()._actualFood;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetKeyDown (KeyCode.Q)) {
			//Application.Quit ();
			Analytics.CustomEvent ("Quit Game ", new Dictionary<string, object>
		                       {
			{ "Oxygen ", _oxygen },
			{ "Water ", _water },
			{ "Food ", _food }
		});
		}

	}

	void OnApplicationQuit ()
	{
		Analytics.CustomEvent ("Quit Game ", new Dictionary<string, object>
		                       {
			{ "Oxygen ", _oxygen },
			{ "Water ", _water },
			{ "Food ", _food }
		});
	}
}
