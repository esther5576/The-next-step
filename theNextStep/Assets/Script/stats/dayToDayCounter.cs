using UnityEngine;
using System.Collections;

public class dayToDayCounter : MonoBehaviour 
{
	private float _time;
	private float _days;
	private bool _dayMorningNight = true;

	public float _dayLength = 60;
	
	void LateUpdate () 
	{
		_time += Time.deltaTime;

		if(_time > _dayLength)
		{
			_dayMorningNight = false;
		}

		if(_time > (_dayLength*2))
		{
			_dayMorningNight = true;
			_days += 1;
			_time = 0;
		}
	}

	private void OnGUI()
	{
		if(_dayMorningNight == true)
		{
			GUI.Label(new Rect(565,10, 100, 30), "DAY");
		}
		if(_dayMorningNight == false)
		{
			GUI.Label(new Rect(565,10, 100, 30), "NIGHT");
		}
		GUI.Label(new Rect(575,30, 100, 30), "" + _days);
	}
}
