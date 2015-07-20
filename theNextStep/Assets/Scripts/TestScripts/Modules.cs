﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Modules : MonoBehaviour
{


	public GameObject Model;
	private ModuleCreator Creator;

	public List<GameObject> _buildings = new List<GameObject> ();
	// Use this for initialization
	void Awake ()
	{
		Creator = ModuleCreator.Instance;
		Creator.Initiate (1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			Creator.Deploy (Model);
		}
	}

	public void MainBuilding (bool _MainBuilding)
	{
		Model = _buildings [0];
	}

	public void FoodProduction (bool _FoodProduction)
	{
		Model = _buildings [1];
	}

	public void SolarPanels (bool _SolarPanels)
	{
		Model = _buildings [2];
	}

	public void HumanHabitats (bool _HumanHabitats)
	{
		Model = _buildings [3];
	}

	public void WaterExtractionCenter (bool _WaterExtractionCenter)
	{
		Model = _buildings [4];
	}

	public void LandingZone (bool _LandingZone)
	{
		Model = _buildings [5];
	}
}
