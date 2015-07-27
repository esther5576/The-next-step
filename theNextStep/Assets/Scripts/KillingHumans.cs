using UnityEngine;
using System.Collections;

public class KillingHumans : MonoBehaviour
{

	public int actualDayForFood;
	public bool daySavedForFood;

	public int actualDayForWater;
	public bool daySavedForWater;

	public int actualDayForOxygen;
	public bool daySavedForOxygen;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Camera.main.GetComponent<gameStats> ()._actualHumans >= 1) {
			if (Camera.main.GetComponent<gameStats> ()._actualFood <= 0) {
				if (daySavedForFood == false) {
					actualDayForFood = Camera.main.GetComponent<DayNightController> ()._days;
					daySavedForFood = true;
				}
				if (daySavedForFood == true && actualDayForFood + 5 < Camera.main.GetComponent<DayNightController> ()._days) {
					Camera.main.GetComponent<gameStats> ()._actualHumans --;
					daySavedForFood = false;
				}
			}

			if (Camera.main.GetComponent<gameStats> ()._actualWater <= 0) {
				if (daySavedForWater == false) {
					actualDayForWater = Camera.main.GetComponent<DayNightController> ()._days;
					daySavedForWater = true;
				}
				if (daySavedForWater == true && actualDayForWater + 2 < Camera.main.GetComponent<DayNightController> ()._days) {
					Camera.main.GetComponent<gameStats> ()._actualHumans --;
					daySavedForWater = false;
				}
			}

			if (Camera.main.GetComponent<gameStats> ()._actualOxygen <= 0) {
				if (daySavedForOxygen == false) {
					actualDayForOxygen = Camera.main.GetComponent<DayNightController> ()._days;
					daySavedForOxygen = true;
				}
				if (daySavedForOxygen == true && actualDayForOxygen < Camera.main.GetComponent<DayNightController> ()._days) {
					Camera.main.GetComponent<gameStats> ()._actualHumans --;
					daySavedForOxygen = false;
				}
			}
		}

		if (Camera.main.GetComponent<gameStats> ()._actualHumans == 0) {
			Application.LoadLevel ("GameOver");
		}
	}
}
