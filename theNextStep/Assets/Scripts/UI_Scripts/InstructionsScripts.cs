using UnityEngine;
using System.Collections;

public class InstructionsScripts : MonoBehaviour
{
	public GameObject _slidderSpeedCanvas;
	public GameObject _ConstructionCanvas;
	public GameObject _HUD;
	// Use this for initialization
	void Start ()
	{
		_slidderSpeedCanvas = GameObject.Find ("SlidderForSpeed");
		_ConstructionCanvas = GameObject.Find ("Construction");
		_HUD = GameObject.Find ("HUDstats");

		_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = false;
		
		_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = false;

		_HUD.GetComponent<CanvasGroup> ().alpha = 0;
		_HUD.GetComponent<CanvasGroup> ().interactable = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetMouseButtonDown (1)) {
			this.GetComponent<CanvasGroup> ().alpha = 0;
			this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			this.GetComponent<CanvasGroup> ().interactable = false;

			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_slidderSpeedCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_ConstructionCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_ConstructionCanvas.GetComponent<CanvasGroup> ().interactable = true;

			_HUD.GetComponent<CanvasGroup> ().alpha = 1;
			_HUD.GetComponent<CanvasGroup> ().interactable = true;
		}

	}
}
