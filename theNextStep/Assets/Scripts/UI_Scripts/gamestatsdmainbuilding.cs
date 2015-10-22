using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gamestatsdmainbuilding : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.GetComponent<Text> ().text = "Humans   " + (int)Camera.main.GetComponent<gameStats> ()._actualHumans + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxHumans + "\n\nElectricity   " + (int)Camera.main.GetComponent<gameStats> ()._actualElectricity + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxElectricity + "\n\nOxygen   " + (int)Camera.main.GetComponent<gameStats> ()._actualOxygen + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxOxygen + "\n\nWater   " + (int)Camera.main.GetComponent<gameStats> ()._actualWater + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxWater + "\n\nFood   " + (int)Camera.main.GetComponent<gameStats> ()._actualFood + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxFood + "\n\nBuilding materials   " + (int)Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxConstructionMaterials + "\n\nRepair materials   " + (int)Camera.main.GetComponent<gameStats> ()._actualReparationMaterials + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxReparationMaterials + "\n\nUpgrade materials   " + (int)Camera.main.GetComponent<gameStats> ()._actualUpgradeMaterials + " / " + (int)Camera.main.GetComponent<gameStats> ()._maxUpgradeMaterials;
	}
}
