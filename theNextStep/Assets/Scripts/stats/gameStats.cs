using UnityEngine;
using System.Collections;

public class gameStats : MonoBehaviour
{
	#region actual material
	//Actual amount of material that the player has ====================================================================================================================================================================
	public float _actualElectricity = 500f;
	public int _actualHumans = 10;
	public float _actualTemperature = 20f;

	public float _actualOxygen = 12000f;
	public float _actualWater = 6000f;
	public float _actualFood = 900000f;

	public float _actualConstructionMaterials = 100f;
	public float _actualReparationMaterials = 100f;
	public float _actualUpgradeMaterials = 100f;
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
	
	public float _maxOxygen = 12000f;
	public float _maxWater = 6000f;
	public float _maxFood = 900000f;

	public float _maxConstructionMaterials = 1000f;
	public float _maxReparationMaterials = 1000f;
	public float _maxUpgradeMaterials = 1000f;
	//The minimum amount of energy needed so that everything works =====================================================================================================================================================
	#endregion


	void Update ()
	{
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
