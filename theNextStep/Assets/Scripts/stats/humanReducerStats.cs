using UnityEngine;
using System.Collections;

public class humanReducerStats : MonoBehaviour
{
	//Amount of material used by one human per day =======================================================================================
	public float _waterHumanUse = 20;
	public float _oxygenHumanUse = 40;
	public float _foodHumanUse = 3000;
	//Amount of material used by one human per day =======================================================================================
	
	//Stats for the total amount of stats lost ===========================================================================================
	public float _waterAmountLost;
	public float _oxygenAmountLost;
	public float _foodAmountLost;
	//Stats for the total amount of stats lost ===========================================================================================
	
	
	// Update is called once per frame
	void Update ()
	{
		_waterAmountLost += (_waterHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
		_oxygenAmountLost += (_oxygenHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
		_foodAmountLost += (_foodHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
		
		this.GetComponent<gameStats> ()._actualWater -= (_waterHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
		this.GetComponent<gameStats> ()._actualOxygen -= (_oxygenHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
		this.GetComponent<gameStats> ()._actualFood -= (_foodHumanUse / (Camera.main.GetComponent<DayNightController> ().secondsInFullDay / Camera.main.GetComponent<DayNightController> ().timeMultiplier)) * Time.deltaTime * this.GetComponent<gameStats> ()._actualHumans;
	}
}