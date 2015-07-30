using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class otherStatsShow : MonoBehaviour
{
	public GameObject _statsLeft;
	// Use this for initialization
	void Start ()
	{
		_statsLeft = GameObject.Find ("HUDstatsTextLeft");
	}
	
	// Update is called once per frame
	void Update ()
	{
		_statsLeft.GetComponent<Text> ().text = "Humans: " + (int)this.GetComponent<gameStats> ()._actualHumans + "\n\nElectricity: " + (int)this.GetComponent<gameStats> ()._actualElectricity + "\n\nUpgrade materials: " + (int)this.GetComponent<gameStats> ()._actualUpgradeMaterials + "\n\nReparation materials: " + (int)this.GetComponent<gameStats> ()._actualReparationMaterials;
	}

	/*void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.white;

		GUI.Label (new Rect (25, 20, 500, 30), "Humans: " + this.GetComponent<gameStats> ()._actualHumans, myStyle);
		GUI.Label (new Rect (25, 50, 500, 30), "Electricity: " + this.GetComponent<gameStats> ()._actualElectricity, myStyle);
		//GUI.Label (new Rect (25, 80, 500, 30), "Minerals: " + this.GetComponent<gameStats> ()._actualMineral, myStyle);

		GUI.Label (new Rect (25, 110, 500, 30), "Temperature: " + this.GetComponent<gameStats> ()._actualTemperature, myStyle);
	}*/
}
