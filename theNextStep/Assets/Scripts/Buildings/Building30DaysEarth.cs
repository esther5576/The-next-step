using UnityEngine;
using System.Collections;

public class Building30DaysEarth : MonoBehaviour
{

	public int _daysDrop;

	// Use this for initialization
	void Start ()
	{
		_daysDrop = 30;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Camera.main.GetComponent<DayNightController> ().currentTimeOfDay == 0) {
			_daysDrop --;
		}

		if (_daysDrop == 0) {
			Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials += 1500;
			Camera.main.GetComponent<gameStats> ()._actualHumans += 5;
			_daysDrop = 30;
		}
	}
}
