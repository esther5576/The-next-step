using UnityEngine;
using System.Collections;

public class buildingDurability : MonoBehaviour
{
	public float _actualDurability = 100;
	public float _maximumDurability = 100;
	public float _durabilityHalfProduction = 25;
	public float _durabilityBroken = 0;
	
	public float secondsInFullDay = 120f;
	public float currentTimeOfDay = 0;
	// A multiplier other scripts can use to speed up and slow down the passing of time.
	[HideInInspector]
	public float
		timeMultiplier = 1f;
	
	[HideInInspector]
	public bool
		_halfProd;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// This makes currentTimeOfDay go from 0 to 1 in the number of seconds we've specified.
		currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
		
		// If currentTimeOfDay is 1 (midnight) set it to 0 again so we start a new day.
		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
			_actualDurability--;
		}
		
		if (_actualDurability < _durabilityHalfProduction) {
			_halfProd = true;
		} else {
			_halfProd = false;
		}
		
		if (_actualDurability < _durabilityBroken) {
			Debug.Log ("buildingNotWorking");
			this.GetComponent<ProductionAndNeeds> ()._buildingBroken = true;
		} else {
			this.GetComponent<ProductionAndNeeds> ()._buildingBroken = false;
		}
	}
	
	//100 a 25 produit au 100%
	//25 a 0 produit 50%
	//0inutilisable et le cout de reparation est beaucoup plus cher
}