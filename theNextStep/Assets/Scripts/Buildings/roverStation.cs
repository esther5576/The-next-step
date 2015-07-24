using UnityEngine;
using System.Collections;

public class roverStation : MonoBehaviour
{
	public GameObject _roverCanvas;
	// Use this for initialization
	void Start ()
	{
		_roverCanvas = GameObject.Find ("RoverStationCanvas");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.rigidbody != null && (hit.transform.name == "RoverStation(Clone)" || hit.transform.name == "RoverStation")) {
					_roverCanvas.GetComponent<CanvasGroup> ().alpha = 1;
					_roverCanvas.GetComponent<CanvasGroup> ().interactable = true;
				}
			}
		}
		if (Input.GetMouseButtonDown (1)) {
			_roverCanvas.GetComponent<CanvasGroup> ().alpha = 0;
			_roverCanvas.GetComponent<CanvasGroup> ().interactable = false;
			Time.timeScale = 1;
			Camera.main.GetComponent<DayNightController> ().timeMultiplier = 1;
		}
	}

	public void openRoverMap (bool _openMap)
	{
		//Time.timeScale = 0;
		Camera.main.GetComponent<DayNightController> ().timeMultiplier = 0.1f;
	}
}
