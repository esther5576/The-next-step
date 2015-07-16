using UnityEngine;
using System.Collections;

public class otherStatsShow : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (25, 20, 500, 30), "Humans: " + this.GetComponent<gameStats> ()._actualHumans);
		GUI.Label (new Rect (25, 40, 500, 30), "Electricity: " + this.GetComponent<gameStats> ()._actualElectricity);
		GUI.Label (new Rect (25, 60, 500, 30), "Minerals: " + this.GetComponent<gameStats> ()._actualMineral);

		GUI.Label (new Rect (25, 80, 500, 30), "Temperature: " + this.GetComponent<gameStats> ()._actualTemperature);
	}
}
