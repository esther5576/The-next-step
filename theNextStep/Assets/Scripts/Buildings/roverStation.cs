using UnityEngine;
using System.Collections;

public class roverStation : MonoBehaviour
{
	public GameObject _roverCanvas;
	public GameObject _sliderTime;
	public GameObject _mapCanvas;
	public GameObject _zonesCanvas;
	public GameObject _nextPartCanvas;

	public bool _roverOut;
	// Use this for initialization
	void Start ()
	{
		_roverCanvas = GameObject.Find ("RoverStationCanvas");
		_sliderTime = GameObject.Find ("SlidderForSpeed");
		_mapCanvas = GameObject.Find ("MapRoverCanvas");
		_zonesCanvas = GameObject.Find ("Zones");
		_nextPartCanvas = GameObject.Find ("NextPart");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_roverOut == false) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					if (hit.rigidbody != null && (hit.transform.name == "RoverStation(Clone)" || hit.transform.name == "RoverStation")) {
						_roverCanvas.GetComponent<CanvasGroup> ().alpha = 1;
						_roverCanvas.GetComponent<CanvasGroup> ().interactable = true;
						_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 0;
						_sliderTime.GetComponent<CanvasGroup> ().alpha = 0;
						_sliderTime.GetComponent<CanvasGroup> ().interactable = false;
					}
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			_roverCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_roverCanvas.GetComponent<CanvasGroup> ().interactable = false;
			_sliderTime.GetComponent<slowAndFastMotion> ()._speed = 1;
			_sliderTime.GetComponent<CanvasGroup> ().alpha = 1;
			_sliderTime.GetComponent<CanvasGroup> ().interactable = true;

			_mapCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_mapCanvas.GetComponent<CanvasGroup> ().interactable = false;

			_zonesCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_zonesCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_nextPartCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_nextPartCanvas.GetComponent<CanvasGroup> ().interactable = false;
		}
	}

}
