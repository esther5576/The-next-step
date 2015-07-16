using UnityEngine;
using System.Collections;

public class ProductionAndNeeds : MonoBehaviour
{
	#region Material needs per day
	public float _waterNeedsPerDay = 0;
	public float _electricityNeedsPerDay = 0;
	public float _CO2NeedsPerDay = 0;
	public float _O2NeedsPerDay = 0;
	public float _mineralsNeedsPerDay = 0;
	public float _foodNeedsPerDay = 0;
	public float _HumansNeeds = 0;
	#endregion

	#region Material production per day
	public float _waterProductionPerDay = 0;
	public float _electricityProductionPerDay = 0;
	public float _CO2ProductionPerDay = 0;
	public float _O2ProductionPerDay = 0;
	public float _mineralsProductionPerDay = 0;
	public float _foodProductionPerDay = 0;
	public float _HumansProduction = 0;
	#endregion

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		needs ();
		production ();
	}

	#region Needs
	void needs ()
	{
		if (_waterNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualWater -= ((_waterNeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}

		if (_electricityNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualElectricity -= ((_electricityNeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}

		/*if (_CO2NeedsPerDay != 0) {
			//Camera.main.GetComponent<gameStats> (). -= ((_CO2NeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}*/

		if (_O2NeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualOxygen -= ((_O2NeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}

		if (_mineralsNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualMineral -= ((_mineralsNeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}

		if (_foodNeedsPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualFood -= ((_foodNeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}
	}
	#endregion

	#region Production
	void production ()
	{
		if (_waterProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualWater += ((_waterProductionPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}
		
		if (_electricityProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualElectricity += ((_electricityProductionPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}
		
		/*if (_CO2NeedsPerDay != 0) {
			//Camera.main.GetComponent<gameStats> (). -= ((_CO2NeedsPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}*/
		
		if (_O2ProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualOxygen += ((_O2ProductionPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}
		
		if (_mineralsProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualMineral += ((_mineralsProductionPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}

		if (_foodProductionPerDay != 0) {
			Camera.main.GetComponent<gameStats> ()._actualFood += ((_foodProductionPerDay / (this.GetComponent<dayToDayCounter> ()._dayLength * 2)) * Time.deltaTime);
		}
	}
	#endregion

}
