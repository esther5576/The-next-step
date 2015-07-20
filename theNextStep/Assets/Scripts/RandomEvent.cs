using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEvent : MonoBehaviour
{
	public int _minDay = 10;
	public int _maxDay = 30;
	public bool _activateRandomEvent;

	public List<string> _nameEvent = new List<string> ();

	private int _dayEvent;
	private int _randomEvent;

	// Use this for initialization
	void Start ()
	{
	



	}

	// Update is called once per frame
	void Update ()
	{
	
		//float randomPoint = Random.value * 10;
		//Debug.Log (Mathf.CeilToInt (randomPoint));
		if (_activateRandomEvent == false) {
			_dayEvent = (Random.Range (_minDay, _maxDay)) + this.GetComponent<dayToDayCounter> ()._days;
			_activateRandomEvent = true;
			//Debug.Log (_dayEvent);
		}

		if (_dayEvent == this.GetComponent<dayToDayCounter> ()._days) {
			_activateRandomEvent = false;
			_randomEvent = Mathf.FloorToInt (Random.value * (_nameEvent.Count));
			crisisEvents ();
		}
			
		//Debug.Log (Mathf.RoundToInt (Random.value * (_nameEvent.Count)));
	}

	void crisisEvents ()
	{
		if (_nameEvent [_randomEvent] == "Dust storm") {
			Debug.Log ("Dust storm");
		}

		if (_nameEvent [_randomEvent] == "Asteroid storm") {
			Debug.Log ("Asteroid storm");
		}

		if (_nameEvent [_randomEvent] == "Epidemic") {
			Debug.Log ("Epidemic");
		}

		if (_nameEvent [_randomEvent] == "Mental breakdown") {
			Debug.Log ("Mental breakdown");
		}

		if (_nameEvent [_randomEvent] == "Native pathogens") {
			Debug.Log ("Native pathogens");
		}

		if (_nameEvent [_randomEvent] == "Radiation poisoning") {
			Debug.Log ("Radiation poisoning");
		}
	}
}
