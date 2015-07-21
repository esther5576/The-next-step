using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Modules : MonoBehaviour
{


	public GameObject Model;
	private ModuleCreator Creator;

	public List<GameObject> _buildings = new List<GameObject> ();
	public int _maximumBuildings = 10;
	// Use this for initialization
	void Awake ()
	{
		Creator = ModuleCreator.Instance;
		Creator.Initiate (1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*if (Input.GetKeyDown (KeyCode.A) && _maximumBuildings > 0) {
			Creator.Deploy (Model);
		}*/
	}

	public void MainBuilding (bool _MainBuilding)
	{
		Model = _buildings [0];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	public void FoodProduction (bool _FoodProduction)
	{
		Model = _buildings [1];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	public void SolarPanels (bool _SolarPanels)
	{
		Model = _buildings [2];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	public void HumanHabitats (bool _HumanHabitats)
	{
		Model = _buildings [3];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	public void WaterExtractionCenter (bool _WaterExtractionCenter)
	{
		Model = _buildings [4];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	public void LandingZone (bool _LandingZone)
	{
		Model = _buildings [5];

		if (_maximumBuildings > 0) {
			Creator.Deploy (Model);
		}
	}

	void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 25;
		myStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width - 200, 100, 500, 30), "buildings left: " + _maximumBuildings, myStyle);
	}
}
