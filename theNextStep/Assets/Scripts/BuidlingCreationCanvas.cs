using UnityEngine;
using System.Collections;

public class BuidlingCreationCanvas : MonoBehaviour
{

	public GameObject _buildings;
	public GameObject _sliderTime;
	public GameObject _constructionCanvas;
	// Use this for initialization
	void Start ()
	{
		_buildings = GameObject.Find ("Buildings");
		_sliderTime = GameObject.Find ("SlidderForSpeed");
		_constructionCanvas = GameObject.Find ("Construction");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (1)) {
			_buildings.GetComponent<CanvasGroup> ().alpha = 0;
			_buildings.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			_buildings.GetComponent<CanvasGroup> ().interactable = false;

			_sliderTime.GetComponent<CanvasGroup> ().alpha = 1;
			_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_sliderTime.GetComponent<CanvasGroup> ().interactable = true;

			_constructionCanvas.GetComponent<CanvasGroup> ().alpha = 1;
			_constructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			_constructionCanvas.GetComponent<CanvasGroup> ().interactable = true;
		}
	}

	public void ActivateBuildings ()
	{
		_buildings.GetComponent<CanvasGroup> ().alpha = 1;
		_buildings.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		_buildings.GetComponent<CanvasGroup> ().interactable = true;

		_sliderTime.GetComponent<CanvasGroup> ().alpha = 0;
		_sliderTime.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		_sliderTime.GetComponent<CanvasGroup> ().interactable = false;

		_constructionCanvas.GetComponent<CanvasGroup> ().alpha = 0;
		_constructionCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		_constructionCanvas.GetComponent<CanvasGroup> ().interactable = false;
	}
}
