using UnityEngine;
using System.Collections;

public class ProductionAndNeeds : MonoBehaviour
{
	//Ceci correspond aux variables que dont le batiment a besoin sur l'inspecteur on defini les variables
	#region Material needs per day
	public float _waterNeedsPerDay = 0;
	public float _electricityNeedsPerDay = 0;
	public float _CO2NeedsPerDay = 0;
	public float _O2NeedsPerDay = 0;
	public float _mineralsNeedsPerDay = 0;
	public float _foodNeedsPerDay = 0;
	public float _HumansNeeds = 0;
	#endregion
	
	//Ceci correspond aux variables que dont le batiment a besoin sur l'inspecteur on defini les variables
	#region Material production per day
	public float _waterProductionPerDay = 0;
	public float _electricityProductionPerDay = 0;
	public float _CO2ProductionPerDay = 0;
	public float _O2ProductionPerDay = 0;
	public float _mineralsProductionPerDay = 0;
	public float _foodProductionPerDay = 0;
	public float _HumansProduction = 0;
	#endregion
	
	#region Material production per day used in the script
	public float _actualWaterProductionPerDay = 0;
	public float _actualElectricityProductionPerDay = 0;
	public float _actualCO2ProductionPerDay = 0;
	public float _actualO2ProductionPerDay = 0;
	public float _actualMineralsProductionPerDay = 0;
	public float _actualFoodProductionPerDay = 0;
	#endregion
	
	//Cette bool correspond au switch on et off du batiment
	[HideInInspector]
	public bool
		_buildingSwitch;
	
	[HideInInspector]
	public bool
		_buildingBroken;
	
	[HideInInspector]
	public bool
		_buildingHalfProd;

	[HideInInspector]
	public bool
		_buildingElectricity = true;
	// Use this for initialization
	void Start ()
	{
		//au debut le batiment est active
		_buildingSwitch = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Les needs et productions sont actifs si le batiment n'est pas une visualisation(=quand on va placer le batiment)
		//et la bool buildingSwitch est true
		if (this.name != "visualisation" && _buildingSwitch == true && _buildingBroken == false && _buildingElectricity == true) {
			needs ();
			production ();
		}

		//building can have electricity
		if (Camera.main.GetComponent<gameStats> ()._actualElectricity <= 0) {
			_buildingElectricity = false;
		} else {
			_buildingElectricity = true;
			Camera.main.GetComponent<gameStats> ()._actualElectricity = 0;
		}

		//Change production with the building durability
		if (this.GetComponent<buildingDurability> ()._halfProd == true) {
			_actualWaterProductionPerDay = _waterProductionPerDay / 2;
			_actualElectricityProductionPerDay = _electricityProductionPerDay / 2;
			_actualCO2ProductionPerDay = _CO2ProductionPerDay / 2;
			_actualO2ProductionPerDay = _O2ProductionPerDay / 2;
			_actualMineralsProductionPerDay = _mineralsProductionPerDay / 2;
			_actualFoodProductionPerDay = _foodProductionPerDay / 2;
		}
		if (this.GetComponent<buildingDurability> ()._halfProd == false) {
			_actualWaterProductionPerDay = _waterProductionPerDay;
			_actualElectricityProductionPerDay = _electricityProductionPerDay;
			_actualCO2ProductionPerDay = _CO2ProductionPerDay;
			_actualO2ProductionPerDay = _O2ProductionPerDay;
			_actualMineralsProductionPerDay = _mineralsProductionPerDay;
			_actualFoodProductionPerDay = _foodProductionPerDay;
		}
	}
	
	//Cette fonction est appellee quand le boutton est appuye
	public void buildingActivation ()
	{
		_buildingSwitch = !_buildingSwitch;
	}
	
	#region Needs
	void needs ()
	{
		if (_waterNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualWater -= ((_waterNeedsPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		if (_electricityNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualElectricity -= ((_electricityNeedsPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		/*if (_CO2NeedsPerDay != 0) {
			//Camera.main.GetComponent<gameStats> (). -= ((_CO2NeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}*/
		
		if (_O2NeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualOxygen -= ((_O2NeedsPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		/*if (_mineralsNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualMineral -= ((_mineralsNeedsPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}*/
		
		if (_foodNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualFood -= ((_foodNeedsPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
	}
	#endregion
	
	#region Production
	void production ()
	{
		if (_waterProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualWater += ((_actualWaterProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		if (_electricityProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualElectricity += ((_actualElectricityProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		/*if (_CO2NeedsPerDay != 0) {
			//Camera.main.GetComponent<gameStats> (). -= ((_actualCO2ProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}*/
		
		if (_O2ProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualOxygen += ((_actualO2ProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
		
		/*if (_mineralsProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualMineral += ((_actualMineralsProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}*/
		
		if (_foodProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualFood += ((_actualFoodProductionPerDay / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime);
		}
	}
	#endregion
	
}