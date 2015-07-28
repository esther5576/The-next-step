using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameStats : MonoBehaviour
{
	#region actual material
	//Actual amount of material that the player has ====================================================================================================================================================================
	public float _actualElectricity = 300f;
	public int _actualHumans = 10;
	public float _actualTemperature = 20f;

	public float _actualOxygen = 200f;
	public float _actualWater = 200f;
	public float _actualFood = 200f;

	public float _actualConstructionMaterials = 1500f;
	public float _actualReparationMaterials = 500f;
	public float _actualUpgradeMaterials = 500f;
	//Actual amount of material that the player has ====================================================================================================================================================================
	#endregion

	#region minimum material
	//The minimum amount of energy needed so that everything works =====================================================================================================================================================
	public float _minElectricity = 1f;
	public int _minHumans = 1;
	public float _minTemperature = 1f;
	
	public float _minOxygen = 1f;
	public float _minWater = 1f;
	public float _minFood = 1f;

	public float _minConstructionMaterials = 1f;
	public float _minReparationMaterials = 1f;
	public float _minUpgradeMaterials = 1f;
	//The minimum amount of energy needed so that everything works =====================================================================================================================================================
	#endregion

	#region maximum material
	//The minimum amount of energy needed so that everything works =====================================================================================================================================================
	public float _maxElectricity = 500f;
	public int _maxHumans = 10;
	public float _maxTemperature = 60f;
	
	public float _maxOxygen = 300f;
	public float _maxWater = 300f;
	public float _maxFood = 300f;

	public float _maxConstructionMaterials = 2000f;
	public float _maxReparationMaterials = 2000f;
	public float _maxUpgradeMaterials = 2000f;
	//The minimum amount of energy needed so that everything works =====================================================================================================================================================
	#endregion

	public  List<GameObject> _numberOfWarehouses = new List<GameObject> ();
	public int _intOfWarehouses;
	void Update ()
	{
		#region Tout ceci est pour les warehouses et leurs effets
		//Tout ceci est pour les warehouses et leurs effets
		for (var i = _numberOfWarehouses.Count - 1; i > -1; i--) {
			if (_numberOfWarehouses [i] == null)
				_numberOfWarehouses.RemoveAt (i);
		}

		_intOfWarehouses = _numberOfWarehouses.Count;

		_maxElectricity = 500f + (500f / 20) * _intOfWarehouses;
		_maxOxygen = 300f + (300f / 20) * _intOfWarehouses;
		_maxWater = 300f + (300f / 20) * _intOfWarehouses;
		_maxFood = 300f + (300f / 20) * _intOfWarehouses;
		_maxConstructionMaterials = 2000f + (1000f / 20) * _intOfWarehouses;
		_maxReparationMaterials = 2000f + (1000f / 20) * _intOfWarehouses;
		_maxUpgradeMaterials = 2000f + (1000f / 20) * _intOfWarehouses;
		#endregion

		//Redmetrics When the night end send stats
		if (Camera.main.GetComponent<DayNightController> ().currentTimeOfDay == 0) {
			RedMetricsManager.get ().sendEvent (TrackingEvent.STATS, "{\"Oxygen\": " + _actualOxygen + ", \"Water\": " + _actualWater + ", \"Food\": " + _actualFood + ", \"Days\": " + Camera.main.GetComponent<DayNightController> ()._days + "}");
		}
		//Redmetrics When the night end send stats


		if (_actualElectricity > _maxElectricity) {
			_actualElectricity = _maxElectricity;
		}

		if (_actualFood > _maxFood) {
			_actualFood = _maxFood;
		}

		if (_actualHumans > _maxHumans) {
			_actualHumans = _maxHumans;
		}

		if (_actualOxygen > _maxOxygen) {
			_actualOxygen = _maxOxygen;
		}

		if (_actualWater > _maxWater) {
			_actualWater = _maxWater;
		}

		if (_actualTemperature > _maxTemperature) {
			_actualTemperature = _maxTemperature;
		}

		if (_actualConstructionMaterials > _maxConstructionMaterials) {
			_actualConstructionMaterials = _maxConstructionMaterials;
		}

		if (_actualReparationMaterials > _maxReparationMaterials) {
			_actualReparationMaterials = _maxReparationMaterials;
		}

		if (_actualUpgradeMaterials > _maxUpgradeMaterials) {
			_actualUpgradeMaterials = _maxUpgradeMaterials;
		}
	}
}
