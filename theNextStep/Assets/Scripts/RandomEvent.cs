using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour
{
	public int _minDay = 10;
	public int _maxDay = 30;
	public bool _activateRandomEvent;
	
	public List<string> _nameEvent = new List<string> ();
	
	private int _dayEvent;
	private int _randomEvent;

	public GameObject _crisisCanvas;
	public GameObject _crisisText;

	public GameObject _slidderSpeedCanvas;
	public GameObject _ConstructionCanvas;
	public GameObject _HUD;
	// Use this for initialization
	void Start ()
	{
		_crisisCanvas = GameObject.Find ("CrisisPanel");
		_crisisText = GameObject.Find ("CrisisInstruction");

		_slidderSpeedCanvas = GameObject.Find ("SlidderForSpeed");
		_ConstructionCanvas = GameObject.Find ("Construction");
		_HUD = GameObject.Find ("HUDstats");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//float randomPoint = Random.value * 10;
		//Debug.Log (Mathf.CeilToInt (randomPoint));
		if (_activateRandomEvent == false) {
			_dayEvent = (Random.Range (_minDay, _maxDay)) + this.GetComponent<DayNightController> ()._days;
			_activateRandomEvent = true;
			//Debug.Log (_dayEvent);
		}
		
		if (_dayEvent == this.GetComponent<DayNightController> ()._days && _activateRandomEvent == true) {
			_activateRandomEvent = false;
			_randomEvent = Mathf.FloorToInt (Random.value * (_nameEvent.Count));
			crisisEvents ();
		}
		
		//Debug.Log (Mathf.RoundToInt (Random.value * (_nameEvent.Count)));
	}
	
	void crisisEvents ()
	{
		if (_nameEvent [_randomEvent] == "Dust storm") {

			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_slidderSpeedCanvas.GetComponent<slowAndFastMotion> ()._speed = 1;
			
			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
			
			_HUD.GetComponent<CanvasGroup> ().alpha = 0;
			_HUD.GetComponent<CanvasGroup> ().interactable = false;

			_crisisText.GetComponent<Text> ().text = "<b><size=25>A storm is coming…</size></b>\n\n<i>You know what gets everywhere? Dust. Dust gets everywhere.</i>\n\nA dust storm has picked up. While nowhere near as dangerous as Earth storms, due to the very low atmospheric pressure here compared to Earth (less than 1%), it kicks up enough dust to reduce sun exposure significantly, and cover everything.\n\nDust storms usually last a few days, with winds blowing from around 50 to 110 km/h. However, again due to the low atmospheric pressure, such fast winds will only be felt as a light breeze. They can also sometimes last several weeks or months, and cover significant portions of the planet.";
			Debug.Log ("Dust storm");
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("building"); 
			gos = GameObject.FindGameObjectsWithTag ("Pipeline"); 
			if (gos.Length != 0) {
				int _randomBuilding;
				_randomBuilding = Mathf.FloorToInt (Random.value * (gos.Length));
				gos [_randomBuilding].GetComponent<buildingDurability> ()._actualDurability -= 25;
			}
		}
		
		if (_nameEvent [_randomEvent] == "Asteroid storm") {
			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_slidderSpeedCanvas.GetComponent<slowAndFastMotion> ()._speed = 1;
			
			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
			
			_HUD.GetComponent<CanvasGroup> ().alpha = 0;
			_HUD.GetComponent<CanvasGroup> ().interactable = false;

			_crisisText.GetComponent<Text> ().text = "<b><size=25>Meteor? Meteoroid? Meteorite?</size></b>\n\n<i>That’s going to leave a dent...</i>\n\nAn asteroid fragment (or meteoroid) hit the atmosphere at 15km/sec and fragmented. The resulting meteoroid shower was very pretty. Additionally, it has now hit your settlement, causing significant damage to some of your installations.\n\nA meteor shower would have been preferable: even prettier, but the higher speed of the comet fragments that produce meteor showers would have meant complete burn-up in the atmosphere, and no damage to your colony.";
			Debug.Log ("Asteroid storm");
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("building"); 
			gos = GameObject.FindGameObjectsWithTag ("Pipeline"); 
			if (gos.Length != 0) {
				int _randomBuilding;
				_randomBuilding = Mathf.FloorToInt (Random.value * (gos.Length));
				gos [_randomBuilding].GetComponent<buildingDurability> ()._actualDurability = 0;
			}
		}
		
		if (_nameEvent [_randomEvent] == "Epidemic") {
			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_slidderSpeedCanvas.GetComponent<slowAndFastMotion> ()._speed = 1;
			
			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
			
			_HUD.GetComponent<CanvasGroup> ().alpha = 0;
			_HUD.GetComponent<CanvasGroup> ().interactable = false;

			_crisisText.GetComponent<Text> ().text = "<b><size=25>There’s something going around.</size></b>\n\n<i>It’s just a Martian sniffle! What’s the worst that could happen?</i>\n\nOne of the colonists is sick. You have no idea what it is, how bad it could get, and how contagious it is.";
			Debug.Log ("Epidemic");

			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("medical"); 
			if (gos.Length == 0) {
				this.GetComponent<gameStats> ()._actualHumans --;
			}
		}
		
		if (_nameEvent [_randomEvent] == "Solar Particle Event") {
			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_slidderSpeedCanvas.GetComponent<slowAndFastMotion> ()._speed = 1;
			
			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
			
			_HUD.GetComponent<CanvasGroup> ().alpha = 0;
			_HUD.GetComponent<CanvasGroup> ().interactable = false;

			_crisisText.GetComponent<Text> ().text = "<b><size=25>Proton Storm!</size></b>\n\n<i>So shiny... It tingles!</i>\n\nA solar particle event has sent large amounts of particles (mostly protons) into interplanetary space, following a solar flare. One of your settlers was caught out in the open when this proton storm hit Mars, and received a large dose of radiation as a result. They reported seeing white flashes of light, as energetic protons interacted with their optical tissues.\n\nAdditionally, the highly energetic protons have electrically charged your rover, causing it to go offline, and damaged some of the electronic components in your colony.";
			Debug.Log ("Solar Particle Event");
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("RoverStation"); 
			if (gos.Length != 0) {
				foreach (GameObject go in gos) {
					go.GetComponent<roverStation> ()._daysOutTotal = 0;
					go.GetComponent<roverStation> ()._daysOut = 0;
				}
			}
		}
		
		if (_nameEvent [_randomEvent] == "Human error") {
			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_slidderSpeedCanvas.GetComponent<slowAndFastMotion> ()._speed = 1;
			
			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
			
			_HUD.GetComponent<CanvasGroup> ().alpha = 0;
			_HUD.GetComponent<CanvasGroup> ().interactable = false;

			_crisisText.GetComponent<Text> ().text = "<b>Oops…</b>\nI wonder what happens if I do this?\nOne of your colonists failed to follow safety precautions and learned first hand why they exist in the first place. One of your installations is now malfunctioning and requires maintenance for it to get back to full production.";
			Debug.Log ("Human error");
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag ("building"); 
			gos = GameObject.FindGameObjectsWithTag ("Pipeline"); 
			if (gos.Length != 0) {
				int _randomBuilding;
				_randomBuilding = Mathf.FloorToInt (Random.value * (gos.Length));
				gos [_randomBuilding].GetComponent<buildingDurability> ()._actualDurability = 0;
			}
		}

		//A IMPLEMENTER
		/*if (_nameEvent [_randomEvent] == "Heating failure") {
			_crisisCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_crisisCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_crisisCanvas.GetComponent<CanvasGroup> ().interactable = true;
			
			_crisisText.GetComponent<Text> ().text = "";
			Debug.Log ("Heating failure");
		}*/
	}
}
