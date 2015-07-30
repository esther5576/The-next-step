using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapRover : MonoBehaviour
{
	[HideInInspector]
	public GameObject
		_roverCanvas;

	[HideInInspector]
	public GameObject
		_zonesCanvas;

	[HideInInspector]
	public GameObject
		_nextPartCanvas;

	[HideInInspector]
	public int
		_zone;

	[HideInInspector]
	public GameObject
		_mapCanvas;

	public GameObject _sliderTime;

	public int
		_numberDays;

	public GameObject _textDays;

	public float _actualNumberDays;

	public GameObject _actualBuildingSelected;

	public GameObject _slidderNextPart;

	public bool _changeDaysNumber;
	// Use this for initialization
	void Start ()
	{
		_roverCanvas = GameObject.Find ("RoverStationCanvas");
		_zonesCanvas = GameObject.Find ("Zones");
		_nextPartCanvas = GameObject.Find ("NextPart");
		_mapCanvas = GameObject.Find ("MapRoverCanvas");
		_sliderTime = GameObject.Find ("SlidderForSpeed");
		_textDays = GameObject.Find ("Days");
		_slidderNextPart = GameObject.Find ("slidderNextPart");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_numberDays != (int)_slidderNextPart.GetComponent<Slider> ().value && _changeDaysNumber == true) {
			_numberDays = (int)_slidderNextPart.GetComponent<Slider> ().value;
			_textDays.GetComponent<Text> ().text = "Number of days: " + _numberDays * 2;
		}

	}

	public void openRoverMap (bool _openMap)
	{
		_roverCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_roverCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_roverCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		_mapCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_mapCanvas.GetComponent<CanvasGroup> ().interactable = true;
		_mapCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;

		_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_zonesCanvas.GetComponent<CanvasGroup> ().interactable = true;
		_zonesCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		
		_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_nextPartCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void topLeftPartOfTheMap (bool _openZone1)
	{
		canvasGroupChange ();
		_zone = 1;
	}

	public void topRightPartOfTheMap (bool _openZone2)
	{
		canvasGroupChange ();
		_zone = 2;
	}

	public void bottomLeftPartOfTheMap (bool _openZone3)
	{
		canvasGroupChange ();
		_zone = 3;
	}

	public void bottomRightPartOfTheMap (bool _openZone4)
	{
		canvasGroupChange ();
		_zone = 4;
	}

	void canvasGroupChange ()
	{
		_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_zonesCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_zonesCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		
		_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 1;
		_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = true;
		_nextPartCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;

		_slidderNextPart.GetComponent<CanvasGroup> ().alpha = 1;
		_slidderNextPart.GetComponent<CanvasGroup> ().interactable = true;
		_slidderNextPart.GetComponent<CanvasGroup> ().blocksRaycasts = true;

		_changeDaysNumber = true;
	}

	public void confirmRover (bool _RoverSent)
	{

		_changeDaysNumber = false;

		_actualNumberDays = _numberDays;

		_actualBuildingSelected.GetComponent<roverStation> ()._daysOut = (int)_actualNumberDays;
		_actualBuildingSelected.GetComponent<roverStation> ()._daysOutTotal = (int)_actualNumberDays * 2;
		_actualBuildingSelected.GetComponent<roverStation> ()._zoneOut = _zone;

		_numberDays = 0;


		_roverCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_roverCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_roverCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
		
		_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 1;
		_sliderTime.GetComponent<CanvasGroup> ().alpha = 1;
		_sliderTime.GetComponent<CanvasGroup> ().interactable = true;
		_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		
		_mapCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_mapCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_mapCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
		
		_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_zonesCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_zonesCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
		_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = false;
		_nextPartCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		_slidderNextPart.GetComponent<CanvasGroup> ().alpha = 0;
		_slidderNextPart.GetComponent<CanvasGroup> ().interactable = false;
		_slidderNextPart.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		_slidderNextPart.GetComponent<Slider> ().value = 0;
	}

	public void adjustDays (int _newDays)
	{

	}
}
