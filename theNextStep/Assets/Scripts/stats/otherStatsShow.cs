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
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.white;

		GUI.Label (new Rect (25, 20, 500, 30), "Humans: " + this.GetComponent<gameStats> ()._actualHumans, myStyle);
		GUI.Label (new Rect (25, 50, 500, 30), "Electricity: " + this.GetComponent<gameStats> ()._actualElectricity, myStyle);
		//GUI.Label (new Rect (25, 80, 500, 30), "Minerals: " + this.GetComponent<gameStats> ()._actualMineral, myStyle);

		GUI.Label (new Rect (25, 110, 500, 30), "Temperature: " + this.GetComponent<gameStats> ()._actualTemperature, myStyle);
	}
}
