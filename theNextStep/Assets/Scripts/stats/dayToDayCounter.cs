using UnityEngine;
using System.Collections;

public class dayToDayCounter : MonoBehaviour
{
	/*private float _time;
	public int _days;
	private bool _dayMorningNight = true;

	public float _dayLength = 60;

	public GameObject _sandEffect;

	void Start ()
	{
		_sandEffect = GameObject.Find ("DustStormEsther");
	}*/

	/*void LateUpdate ()
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

		if (_dayMorningNight == true) {
			_sandEffect.SetActive (true);
		}
		if (_dayMorningNight == false) {
			_sandEffect.SetActive (false);
		}
	}*/

	/*void OnGUI ()
	{
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.white;

		if (_dayMorningNight == true) {
			GUI.Label (new Rect (Screen.width - 100, 20, 100, 30), "DAY", myStyle);
		}
		if (_dayMorningNight == false) {
			GUI.Label (new Rect (Screen.width - 100, 20, 100, 30), "NIGHT", myStyle);
		}
		GUI.Label (new Rect (Screen.width - 75, 50, 100, 30), "" + _days, myStyle);
	}*/
}
