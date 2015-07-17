using UnityEngine;
using System.Collections;

public class dayToDayCounter : MonoBehaviour
{
	private float _time;
	public int _days;
	private bool _dayMorningNight = true;

	public float _dayLength = 60;
	
	void LateUpdate ()
	{
		_time += Time.deltaTime;

		if (_time > _dayLength) {
			_dayMorningNight = false;
		}

		if (_time > (_dayLength * 2)) {
			_dayMorningNight = true;
			_days += 1;
			_time = 0;
		}
	}

	void OnGUI ()
	{
		if (_dayMorningNight == true) {
			GUI.Label (new Rect (Screen.width - 50, 20, 100, 30), "DAY");
		}
		if (_dayMorningNight == false) {
			GUI.Label (new Rect (Screen.width - 50, 20, 100, 30), "NIGHT");
		}
		GUI.Label (new Rect (Screen.width - 40, 40, 100, 30), "" + _days);
	}
}
