//By Esther Berges 
//This goes on the rover station
//it lets you open the canvas concerning the rover station
//By Esther Berges


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class roverStation : MonoBehaviour
{
	public GameObject _wholeCanvas;
	public GameObject _roverCanvas;
	public GameObject _sliderTime;
	public GameObject _mapCanvas;
	public GameObject _zonesCanvas;
	public GameObject _nextPartCanvas;
	public GameObject _slidderNextPart;
	public GameObject _RoverOut;
	public GameObject _TextRoverOut;

	public bool _roverOut;
	public int _daysOut;
	public int _daysOutTotal;
	public int _zoneOut;

	public GameObject _HUDstats;
	public GameObject _constructionButton;

	public int _upgradeMaterialsCol;
	public int _constructionMaterialsCol;
	public int _repairMaterialsCol;
	// Use this for initialization
	void Start ()
	{
		_wholeCanvas = GameObject.Find ("RoverCanvas");
		_roverCanvas = GameObject.Find ("RoverStationCanvas");
		_sliderTime = GameObject.Find ("SlidderForSpeed");

		//Image de la carte
		_mapCanvas = GameObject.Find ("MapRoverCanvas");
		//buttons des zones de la carte
		_zonesCanvas = GameObject.Find ("Zones");
		//envoyer rover + temps
		_nextPartCanvas = GameObject.Find ("NextPart");
		_slidderNextPart = GameObject.Find ("slidderNextPart");
		_RoverOut = GameObject.Find ("RoverOut");

		_TextRoverOut = GameObject.Find ("TextRoverOut");
		_HUDstats = GameObject.Find ("HUDstats");
		_constructionButton = GameObject.Find ("Construction");
	}
	
	// Update is called once per frame
	void Update ()
	{



		if (_daysOutTotal > 0) {
			_roverOut = true;

			if (Camera.main.GetComponent<DayNightController> ().currentTimeOfDay == 0) {
				_daysOut --;
				_daysOutTotal --;

				if (_daysOutTotal > 0) {
					if (_roverOut == true) {
						int _materialCollected = (Random.Range (0, 101));

						switch (_zoneOut) {
						case 1:
							if (_materialCollected >= 0 && _materialCollected <= 20) {
								Camera.main.GetComponent<gameStats> ()._actualReparationMaterials += 20 * (_daysOutTotal / 10);
							}
							if (_materialCollected >= 20 && _materialCollected <= 70) {
								Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials += 40 * (_daysOutTotal / 10);
							}
							break;
							
							
						case 2:
							if (_materialCollected >= 0 && _materialCollected <= 20) {
								Camera.main.GetComponent<gameStats> ()._actualReparationMaterials += 20 * (_daysOutTotal / 10);
							}
							if (_materialCollected >= 20 && _materialCollected <= 70) {
								Camera.main.GetComponent<gameStats> ()._actualUpgradeMaterials += 40 * (_daysOutTotal / 10);
							}
							break;
							
							
						case 3:
							if (_materialCollected >= 0 && _materialCollected <= 20) {
								Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials += 20 * (_daysOutTotal / 10);
							}
							if (_materialCollected >= 20 && _materialCollected <= 90) {
								Camera.main.GetComponent<gameStats> ()._actualReparationMaterials += 40 * (_daysOutTotal / 10);
							}
							break;
							
							
						case 4:
							if (_materialCollected >= 0 && _materialCollected <= 30) {
								Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials += 20 * (_daysOutTotal / 10);
							}
							if (_materialCollected >= 30 && _materialCollected <= 60) {
								Camera.main.GetComponent<gameStats> ()._actualReparationMaterials += 20 * (_daysOutTotal / 10);
							}
							if (_materialCollected >= 60 && _materialCollected <= 90) {
								Camera.main.GetComponent<gameStats> ()._actualUpgradeMaterials += 20 * (_daysOutTotal / 10);
							}
							break;
						}
					}
				}
			}

			if (_daysOutTotal == 0) {
				_roverOut = false;

				//Le faire ici?
				Debug.Log ("J'ai fini");
			}

		}

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.rigidbody != null && (hit.transform.tag == "RoverStation")) {

					_HUDstats.GetComponent<CanvasGroup> ().alpha = 0;

					_constructionButton.GetComponent<CanvasGroup> ().alpha = 0;
					_constructionButton.GetComponent<CanvasGroup> ().blocksRaycasts = false;
					_constructionButton.GetComponent<CanvasGroup> ().interactable = false;

					GameObject _thisRover = hit.transform.gameObject;

					if (_thisRover.GetComponent<roverStation> ()._roverOut == false) {
						_RoverOut.GetComponent<CanvasGroup> ().alpha = 0;
						_RoverOut.GetComponent<CanvasGroup> ().interactable = false;
						_RoverOut.GetComponent<CanvasGroup> ().blocksRaycasts = false;

						_roverCanvas.GetComponent<CanvasGroup> ().alpha = 1;
						_roverCanvas.GetComponent<CanvasGroup> ().interactable = true;
						_roverCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;

						_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 0;
						_sliderTime.GetComponent<CanvasGroup> ().alpha = 0;
						_sliderTime.GetComponent<CanvasGroup> ().interactable = false;
						_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = false;

						_wholeCanvas.GetComponent<MapRover> ()._actualBuildingSelected = _thisRover.gameObject;
					}

					if (_thisRover.GetComponent<roverStation> ()._roverOut == true) {

						_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 1;
						_sliderTime.GetComponent<CanvasGroup> ().alpha = 0;
						_sliderTime.GetComponent<CanvasGroup> ().interactable = false;
						_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = false;

						_TextRoverOut.GetComponent<Text> ().text = "Days out: " + _thisRover.GetComponent<roverStation> ()._daysOutTotal;
						_RoverOut.GetComponent<CanvasGroup> ().alpha = 1;
						_RoverOut.GetComponent<CanvasGroup> ().interactable = true;
						_RoverOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
					}
				}
			}

		}

		if (Input.GetMouseButtonDown (1)) {
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

			_RoverOut.GetComponent<CanvasGroup> ().alpha = 0;
			_RoverOut.GetComponent<CanvasGroup> ().interactable = false;
			_RoverOut.GetComponent<CanvasGroup> ().blocksRaycasts = false;

			_HUDstats.GetComponent<CanvasGroup> ().alpha = 1;

			_constructionButton.GetComponent<CanvasGroup> ().alpha = 1;
			_constructionButton.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_constructionButton.GetComponent<CanvasGroup> ().interactable = true;
			//_roverCanvas.GetComponent<MapRover> ()._actualBuildingSelected = null;
		}
	}

}
