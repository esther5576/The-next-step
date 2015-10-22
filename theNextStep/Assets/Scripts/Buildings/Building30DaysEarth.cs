//By Esther Berges
//This script is on the prefab called Landing zone when the player places a landing zone evry 30 days he can receive stuff from earth
//In this version the drop consist on humand and construction materials
//By Esther Berges
using UnityEngine;
using System.Collections;



public class Building30DaysEarth : MonoBehaviour
{
	private int _daysDrop;

	//This int is the number of days that the player has to wait before getting stuff from earth
	public int _numberOfDaysForDrop = 30;

	void Start ()
	{
		_daysDrop = _numberOfDaysForDrop;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Every morning in the game the number of days is reduced by one
		if (Camera.main.GetComponent<DayNightController> ().currentTimeOfDay == 0) {
			_daysDrop --;
		}

		//When the countdown is equal to 0 the player receives stuff from earth
		if (_daysDrop == 0) {
			Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials += 1500;
			Camera.main.GetComponent<gameStats> ()._actualHumans += 5;
			_daysDrop = _numberOfDaysForDrop;
		}
	}
}
